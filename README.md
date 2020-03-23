##YunnyAlfredBot
YunnyAlfredBot is a prototype of a Microsoft Teams bot, Alfred, that streamlines dev ops scenarios from multiple sources such as github, stackoverflow and emails.
![Image of YunnyAlfredBot](/ReadMeMaterials/YunnyAlfredBot_sample.PNG)

**Note:** You see 'BigYunsEchoBot' as a bot name in the screen shot as I registered the bot to MIcrosoft Teams by using this name. Initially, I simply named it this way as BigYuns is my nickname and I was suing the EchoBot template to create this bot. Unfortunately, I cannot change the bot name once I name it. Don't get confused the bot name with the Teams app name. The app name is 'YunnyAlfredBot', and this is the name you use to @ mention the bot. 
![Mention YunnyAlfredBot](/ReadMeMaterials/YunnyAlfredBot_mention.PNG)

## What does YunnyAlfredBot do? 
YunnyAlfredBot listens to GitHub issues that are assigned to a paritcular peson (We will discuss this part later). When a new issue is filed, YunnyAlfredBot creates a new adaptive card, which contains important metadata about the issue. If there are any updates in the new issue (ex: someone makes a comment, the author edits the issue, the status of the issue changes), YunnyAlfredBot identifies that this is an existing issue, updates the existing adaptive card and shows all activities in a reply chain. 

## Why do we need YunnyAlfredBot?
As I mentioned above, YunnyAlfredBot is a simple prototype of AlfredBot whose premise is to streamline manual work in dev ops handling. To understand the manual work needed in dev ops sceanrios, we need to understand how many software teams are dealing with dev ops work.

Each software team gets customers' bug reports and/or feature requests from various resources. For instance, Microsfot Teams gets many customer feedbacks mainly from github issues, stackoverflow questions and direct emails. As of now, we have a v-team whose main focus is triaging these bug reports or feature requests. In this 'triaging' process, there are three main problems.

1) No bot that listens to multiple sources.
- Currently, Teams has a webhook that notifies any issues/questions/emails that are filed to the Microsoft Teams support for each source. Therefore, the v-team needs to manually figure out the webhook for three or more sources.

2) No effecient ways to get updates from those external sources to Microsoft Teams Client.
- For instance, when a customer files an issue in GitHub Microsoft Teams repo, an adaptive card that contains the metadata about the issue is sent to a configured Teams channel. This is great! But what happens when another customer encounter the same problem and makes a comment on the same github issue? Or what happens when the same issue is resolved so therefore, it is closed by someone in Microsoft Teams Support team? Ideally, we would like to see all those activities as a reply chain under the same adaptive card about the issue. Unfortunately, however, it is not possible for webhooks to send these activities to Microsoft Teams Client. As a result, Microsoft Teams support team needs to manually go to the github issue, check any updates and share those updates under the corresponding adaptive card. 

3) Noisy notifications. 
- Another existing issue with the current webhooks system is that the same issue can create mutliple adaptive cards. For instance, let's say 
one customer sends an email to Microsoft Teams support team, an adaptive card that contains the metadata about the email gets sent to a configured channel. But when another customer replies to the email, a new adaptive card gets created. This is very confusing to the support team as it is hard to tell which one is truly a new issue. 

To tackle these three problems, my team came up with an idea of creating a bot which can get all issues from different resources, can identify a new issue and an existing issue and make a reply chain or proper tag people. Basically, this bot is an intelligent butler to the support team like Alfred to the Batman! (now can you guess where the name of Alfred Bot came from?)  

As there were many unknowns in the beginning, we decided to start small by creating a prototype that can create a new adaptive card for a new issue, update an exisitng adaptive card for an exisiting issue, create a reply chain and tag designated people from one external resource. This small prototype is, now known as, YunnyAlfredBot. 

## How does YunnyAlfredBot address those three problems?
1) No bot that listens to multiple sources.
-> As a simple solution, my team decided to explore Microsoft Power Automate (formly known as Flow). Under the assumption that three resources expose triggering actions in Microsoft Power Automate, I can create http post request to YunnyAlfredBot by using HTTP action. 

2) No effecient ways to get updates from those external sources to Microsoft Teams Client.
-> By leveraging the unique ID of an issue, YunnyAlfredBot identifies what is an existing issue and what is not. If it is a new issue, YunnyAlfredBot makes a proactive message with an adaptive card. If it is an existing issue, it creates a reply chain under the correspoding adaptive card. 

3) Noisy notifications. 
-> Same approach as the problem #2. YunnyAlfredBot saves the unique id of the issue and the activity id of its adaptive card in Azure Cosmos databse. Therefore, when the bot identifies that this is an exsiting issue, it finds the associated activity id and uses it to update the adaptive card. You can find more about the concept of Activity in Bot development in here: [Conversation Basic](https://docs.microsoft.com/en-us/microsoftteams/platform/bots/how-to/conversations/conversation-basics?tabs=dotnet)

3) 
## Takeaways:

# Try YunnyAlfredBot Locally
## Demo Video
## Set up the bot
## Set up the Azure Cosmos DB
## Get your Github Personal Access Token
## Configure Micorsoft Power Automate
## Deploy the bot to Azure
- If you would like to deploy your bot service to azure, see [Deploy your bot to Azure](https://aka.ms/azuredeployment) for a complete list of deployment instructions.

# References related to Bot Framework, Bot Service and Azure
- [Bot Framework Documentation](https://docs.botframework.com)
- [Bot Basics](https://docs.microsoft.com/azure/bot-service/bot-builder-basics?view=azure-bot-service-4.0)
- [Activity processing](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-concept-activity-processing?view=azure-bot-service-4.0)
- [Azure Bot Service Introduction](https://docs.microsoft.com/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0)
- [Azure Bot Service Documentation](https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0)
- [.NET Core CLI tools](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x)
- [Azure CLI](https://docs.microsoft.com/cli/azure/?view=azure-cli-latest)
- [Azure Portal](https://portal.azure.com)
- [Language Understanding using LUIS](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/)
- [Channels and Bot Connector Service](https://docs.microsoft.com/en-us/azure/bot-service/bot-concepts?view=azure-bot-service-4.0)