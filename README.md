##YunnyAlfredBot
YunnyAlfredBot is a prototype of a Microsoft Teams bot, Alfred, that streamlines dev ops scenarios from multiple sources such as github, stackoverflow and emails.
![Image of YunnyAlfredBot](/ReadMeMaterials/YunnyAlfredBot_sample.PNG)

**Note:** You see 'BigYunsEchoBot' as a bot name in the screen shot as I registered the bot to MIcrosoft Teams by using this name. Initially, I simply named it this way as BigYuns is my nickname and I was suing the EchoBot template to create this bot. Unfortunately, I cannot change the bot name once I name it. Don't get confused the bot name with the Teams app name. The app name is 'YunnyAlfredBot', and this is the name you use to @ mention the bot. 
![Mention YunnyAlfredBot](/ReadMeMaterials/YunnyAlfredBot_mention.PNG)

## Why do we need YunnyAlfredBot?
As I mentioned above, YunnyAlfredBot is a simple prototype of AlfredBot whose premise is to streamline manual work in dev ops handling. To understand the manual work needed in dev ops sceanrios, we need to understand how many software teams are dealing with dev ops work.

Each software team gets customers' bug reports and/or feature requests from various resources. For instance, Microsfot Teams gets many customer feedbacks mainly from github issues, stackoverflow questions and direct emails. As of now, we have a v-team whose main focus is triaging these bug reports or feature requests. In this 'triaging' process, there are two main problems.

1) No bot that listens to multiple sources.
- Currently, Teams has a webhook that notifies any issues/questions/emails that are filed to the Microsoft Teams support for each source. Therefore, the v-team needs to manually figure out the webhook for three or more sources.

2) No effecient ways to get updates from those external sources to Microsoft Teams Client.
- For instance, when a customer sends an email to Microsoft Teams support team, an adaptive card that contains the metadata about the email gets sent to a configured channel. But when

## How will Alfred Bot address those two problems?

## Takeways:

# Try YunnyAlfredBot Locally
## Set up the bot
## Set up the Azure Cosmos DB
## Get your Github Personal Access Token
## Configure the Flows
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