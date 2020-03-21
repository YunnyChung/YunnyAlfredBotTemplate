// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Newtonsoft.Json.Linq;
using AdaptiveCards;
using Newtonsoft.Json;
using System.Xml;
using System;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
// because of CS0104 error between Microsoft.Azure.Documents.Attachment and that of Bot Schema.
using AzureDocuments = Microsoft.Azure.Documents;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Documents.Client;
using YunnyEchoBot.Controllers.Helpers;
using Microsoft.AspNetCore.DataProtection;

namespace YunnyEchoBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        private AzureDocuments.IDocumentClient _documentClient;
        private readonly IConfiguration _config;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private string _encryptionKey;
        public EchoBot( AzureDocuments.IDocumentClient documentClient, IConfiguration config, IDataProtectionProvider dataProtectionProvider, String encryptionKey)
        {
            _documentClient = documentClient;
            _config = config;
            _dataProtectionProvider = dataProtectionProvider;
            _encryptionKey = encryptionKey;
        }

        private async Task<ConversationReferenceData> AddConversationReference(Activity activity)
        {
            var conversationReference = activity.GetConversationReference();
            TeamsChannelData channelData = activity.GetChannelData<TeamsChannelData>();
            string channelID = channelData.Team.Id;
            var localConversationReferenceData = new ConversationReferenceData() { ChannelID = channelID, ChannelConversationReferenceObject = conversationReference };

            var databaseName = _config["CosmosDBDatabaseName"];
            var collectionName = _config["CosmosConversationReferenceCollection"];
            var response = await _documentClient.UpsertDocumentAsync(
               UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
               localConversationReferenceData);

            return localConversationReferenceData;
        }

        private Attachment CreateAdpativeCardAttachment(MockData fakePayload)
        {
            AdaptiveCard card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0));
            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = fakePayload.Title,
                Size = AdaptiveTextSize.ExtraLarge
            });

            card.Body.Add(new AdaptiveFactSet()
            {
                Type = "FactSet",
                Facts = new List<AdaptiveFact>()
                        {
                            new AdaptiveFact(){ Title ="Id", Value = fakePayload.Id },
                            new AdaptiveFact(){ Title = "Severity", Value = fakePayload.Severity.ToString() },
                            new AdaptiveFact(){ Title = "Status", Value = fakePayload.Status }
                        }
            });
            card.Actions.Add(new AdaptiveSubmitAction()
            {
                Type = "Action.Submit",
                Title = "Click me for messageBack"
            });

            string jsonObj = card.ToJson();

            Attachment attachmentCard = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = JsonConvert.DeserializeObject(jsonObj)
            };

            return attachmentCard;
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            turnContext.Activity.RemoveRecipientMention();

            switch (turnContext.Activity.Text.Trim())
            {
                case "connect":
                    var endPointReply = new StringBuilder("Please connect your flow to: ");
                    var hostingURL = _config["BotHostingURL"];
                    endPointReply.Append(hostingURL);
                    endPointReply.Append("/flow/messages/");

                    // encrypt the channelID.
                    var protector = _dataProtectionProvider.CreateProtector(_encryptionKey);
                    TeamsChannelData channelData = turnContext.Activity.GetChannelData<TeamsChannelData>();
                    string channelID = channelData.Team.Id;
                    string protectedChannelID = protector.Protect(channelID);
                    endPointReply.Append(protectedChannelID);
                    var endpointReplyActivity = MessageFactory.Text(endPointReply.ToString()); // I can tag people on the card.

                    await turnContext.SendActivityAsync(endpointReplyActivity, cancellationToken);
                    break;
                case "test":
                    MockData fakePayload = new MockData("1234", "This is Test", 3, "Active");

                    // the below commented code is not needed for the actual
                    // case as the information will come as json object.
                    //string fakepayloadjson = jsonconvert.serializeobject(fakepayload);
                    // if it comes as json, deserializing the json object is needed.
                    //var deserializedjsonobject = jsonconvert.deserializeobject(fakepayloadjson);

                    var attachmentCard = CreateAdpativeCardAttachment(fakePayload);
                    var cardReply = MessageFactory.Attachment(attachmentCard);
                    await turnContext.SendActivityAsync(cardReply);

                    IEnumerable<TeamsChannelAccount> members = await TeamsInfo.GetMembersAsync(turnContext, cancellationToken);
                    List<Entity> entities = new List<Entity>();
                    foreach (TeamsChannelAccount member in members)
                    {
                        foreach (string upn in fakePayload.Mentions)
                        {
                            if (String.Compare(member.UserPrincipalName, upn, true) == 0)
                            {
                                // Construct a ChannelAccount Object.
                                ChannelAccount mentionedUser = new ChannelAccount(member.Id, member.Name, member.Role, member.AadObjectId);
                                // Construct a Mention object.
                                var mentionObject = new Mention
                                {
                                    Mentioned = mentionedUser,
                                    Text = $"<at>{XmlConvert.EncodeName(member.Name)}</at>",
                                };
                                entities.Add(mentionObject);
                            }
                        }
                    }
                    // We need to mention everyone in the entities.
                    // Construct a string that is going to be passed to a replyActivity.
                    var replyActivityTextStingBuilder = new StringBuilder();
                    foreach (Mention mentioned in entities)
                    {
                        replyActivityTextStingBuilder.AppendFormat("{0} ", mentioned.Text);
                    }
                    replyActivityTextStingBuilder.Append("Please take a look");

                    var replyActivity = MessageFactory.Text(replyActivityTextStingBuilder.ToString()); // I can tag people on the card.
                    replyActivity.Entities = entities;
                    await turnContext.SendActivityAsync(replyActivity, cancellationToken);

                    // updated version
                    MockData fakePayLoadForUpdate = new MockData("1234", "This is a test for Update", 3, "Closed");
                    Attachment updatedAttachmentCard = CreateAdpativeCardAttachment(fakePayLoadForUpdate);
                    var newActivity = MessageFactory.Attachment(updatedAttachmentCard);
                    newActivity.Id = cardReply.Id;
                    await turnContext.UpdateActivityAsync(newActivity, cancellationToken);

                    break;
                case "setup":
                    await AddConversationReference(turnContext.Activity as Activity);
                    break;
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }

        protected override async Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {

            // Only save the ConversationReference object when a bot is installed.
            var activity = turnContext.Activity;
            if (activity.MembersRemoved == null)
            {
                foreach (var newMember in activity.MembersAdded)
                {
                    if (newMember.Id == activity.Recipient.Id)
                    {
                        await AddConversationReference(turnContext.Activity as Activity);
                    }
                }
            }

            await base.OnConversationUpdateActivityAsync(turnContext, cancellationToken);
        }
    }
}
