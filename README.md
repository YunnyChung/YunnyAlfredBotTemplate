## YunnyAlfredBot
YunnyAlfredBot is a prototype of a Microsoft Teams bot, Alfred, that streamlines dev ops scenarios from multiple sources such as github, stackoverflow and emails.
![Image of YunnyAlfredBot](/ReadMeMaterials/YunnyAlfredBot_sample.PNG)

**Note:** You see 'BigYunsEchoBot' as a bot name in the screen shot as I registered the bot to MIcrosoft Teams by using this name. Initially, I simply named it this way as BigYuns is my nickname and I was suing the EchoBot template to create this bot. Unfortunately, I cannot change the bot name once I name it. Don't get confused the bot name with the Teams app name. The app name is 'YunnyAlfredBot', and this is the name you use to @ mention the bot. 
![Mention YunnyAlfredBot](/ReadMeMaterials/YunnyAlfredBot_mention.PNG)

# More context about YunnyAlfredBot
If you are curious about following questions, please check [AboutYunnyAlfredBot.md](AboutYunnyAlfredBot.md)
- What does YunnyAlfredBot do? 
- Why do we need YunnyAlfredBot?
- How does YunnyAlfredBot address those three problems?
- What did Yunny learn through this project? 

# Try YunnyAlfredBot Locally
## Demo Video
## Set up the bot
1. Reigster the bot with the Bot Framework via App Studio in Microsoft Teams.
* Install App Studio app in Microsoft Teams. 
* Click 'Create an app' and fill out App details first.  
* Go to Bots under Capabilities. 
  * Click 'Set up' and fill out the page. 
  * Copy the Bot ID and a newly generated app passowrd. We need these values later. 
  * For YunnyAlfredBot, you don't need to select anything under Messaging bot and Calling bot. The scope is only for Team. 
  * More details about using App Studio are available [here](https://docs.microsoft.com/en-us/microsoftteams/platform/tutorials/get-started-dotnet-app-studio). 

2. Set up an tunneling software.
* You can use [ngork](https://ngrok.com/) or [TunnelRelay](https://github.com/OfficeDev/microsoft-teams-tunnelrelay). 
  * If you have an azure account, I highly recommend using TunnelRelay to set up tunneling. 
  * You can find how to use TunnelRelay in its github. 
  * How to use ngork for Teams Bot Development: check [here](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/build-and-test/debug)
* Copy the externally addressable URL from your tunneling app. We need it later. 

3. Configure YunnyAlfredBot.sln
* Open YunnyAlfredBot.sln file in Visual Studio.
* Open appsettings.josn file and update it as follows:
  * Replace `ADD_YOUR_BOT_APP_ID` with the Application ID you received while registering your bot.
  * Replace `ADD_YOUR_BOT_APP_PASSWORD` with the App Password that you copied. 
  * Replace `ADD_YOUR_BOT_HOSTING_URL` with the externally addressable URL that you copied from ngork or TunnelRelay. 

## Set up the Azure Cosmos DB
* Go to [Azure Portal](https://portal.azure.com) and register a new Azure Cosmos DB database.
* Choose "Core (SQL)" for API.
* Create a database with name `YunnyAlfredBotConfig` (You can name the database whatever you want.)
* Create the following containers:
  * `ChannelConversationReference` with partition-key as channelId.
  * `ExternalTicketMessageId` with partition-key as externalTicketId.
* Copy the Cosmos DB endpoint URL and paste it into appsettings.json to replace `ADD_COSMOS_DB_ENDPOINT_URL_HERE`.
* Copy the Cosmos DB primary key value and paste it into appsettings.json to replace `ADD_COSMOS_DB_PRIMARY_KEY_HERE`.
* Copy the Cosmos DB database name and paste it into appsettings.json to replace `ADD_COSMOS_DB_DATABASE_NAME`.

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