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
## Prerequsites
* You need to have an Azure subscription to use Azure CosmosDB. 
* You need to be on a plan that supports premium connectors in Microsoft Power Auotmate. If you have a work or school account, and your orgnaization supports Microsoft Power Automate, you will be able to preimum connectors, such as HTTP. 

## Set up the bot
1. Register the bot with the Bot Framework via App Studio in Microsoft Teams.
* Install App Studio app in Microsoft Teams. 
* Click 'Create an app' and fill out App details first.  
* Go to Bots under Capabilities. 
  * Click 'Set up' and fill out the page. 
  * Copy the Bot ID and a newly generated app passowrd. We need these values later. 
  * For YunnyAlfredBot, you don't need to select anything under Messaging bot and Calling bot. The scope is only for Team. 
  * More details about using App Studio are available [here](https://docs.microsoft.com/en-us/microsoftteams/platform/tutorials/get-started-dotnet-app-studio). 
* Don't install the bot yet. 

2. Set up a tunneling software.
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

## Run YunnyAlfredBot
* Build and deploy YunnyAlfredBot. 
* Go back to App Studio and click 'Test and distribute' under Finish. 
* Install the bot in a channel that you would like to install. 
* Once YunnyAlfreBot is installed, do '@YunnyAlfredBot connect'. 
  * This will return a url to the channel. Copy this value as we need it later. 
  ![YunnyAlfredBot connect](/ReadMeMaterials/connect.PNG)

## Get your Github Personal Access Token
You will need a GitHub personal access token to get all data we need from github. 
* Log in to your Github account.
* Go to settings -> Developer Settings -> Personal Access Token.
* Generate a new token. 
  ![Github Access Token](/ReadMeMaterials/GithubAccessTokenPage.PNG)
* Save this new token as we need this value in the next section.

## Configure Micorsoft Power Automate
YunnyAlfredBot exposes an end point(`flow/messages/{encryptedChannelID}`). You need to configure the follwoing two flos so that each of them can send http post request to YunnyAlfredBot's designated end point when an issue is assigned to a person or when an assigned issue is closed. 

* Go to [Microsoft Power Automate](https://preview.flow.microsoft.com).
* Change your settings to `DSRE Exception - Github Connector`.
  * You will need a special persmission to get this environment settings. 
  * Go to [Power Automate Admin Center](https://preview.admin.flow.microsoft.com/environments) -> Data Policies and see the owners of those policies. Contact them and ask for the permission to this settings in Microsoft Power Automate. 
* Download ["[YunnyAlfredBot]Github-AssignToUser"](/ReadMeMaterials/[YunnyAlfredBot]Github-AssignToUser.zip) to your local machine. 
* Once you change the setting, go to 'My Flows'.
* Click 'import' on the top. 
* Upload "[YunnyAlfredBot]Github-AssignToUser" zip file. 
  * After the zip file is uploaded, you will a page like this: 
    ![After uploaded](/ReadMeMaterials/AfterImported.PNG)
* Click a tool icon under 'ACTION' in the same row with 'YunnyAlfredBot GitHub-AssignToAUser Template'. 
  * Change 'Update' under 'Setup' to 'Create as new'.
  * ![Create as new](/ReadMeMaterials/createasnew.PNG)
*  Click a tool icon under 'ACTION' in the same row with 'unicorn-devwriter'. 
  * Select your github account. If your github account does not exist in the list, click 'create new'. 
    ![Change a user account](/ReadMeMaterials/changeuseraccount.PNG)
  * Name your flow as 'YunnyAlfredBot GitHub-AssignToAUser From Template'.
* If you see a grey X for each row, you are all good to go for "[YunnyAlfredBot]Github-AssignToUser" flow.
  ![Import Set Up is done](/ReadMeMaterials/ImportSetupIsDone.PNG)

Repeat the above process for ["[YunnyAlfredBot]GitHub-CloseIssues flow"](/ReadMeMaterials/[YunnyAlfredBot]GitHub-CloseIssues.zip). 

## Set up your Flows. 
* Open 'YunnyAlfredBot GitHub-AssignToAUser From Template' flow.
  * You will see something like this: 
  ![Flow](/ReadMeMaterials/flow.PNG)
* Go to 'Secrets' box -> Click '...' -> Click 'Settings' -> Add your github personal access token as a value of 'githubAuth' -> Click Done.
* Click 'Switch' -> Click 'Case' -> Click 'HTTP 2'.
* In URI section, copy the url that you got by running '@YunnyAlfredBot connect' in Teams client. 
* In body, see 'mentioned' and add the UPNs of people that you would like YunnyAlfredBot to mention. 
  * Note: Only people in the channel where YunnyAlfredBot is installed can be tagged. 
  * You can check a person's UPN by mentioning the personl in the channel. 
    * ex: dmxtst17@microsoft.com 
    ![get upn](/ReadMeMaterials/upn.PNG)
  * The end result should look like this:
    ![end result](/ReadMeMaterials/endresult.PNG)

Repeat the above process for "[YunnyAlfredBot]GitHub-CloseIssues From Template" flow. 

Now you are all good!
For the testing purpose, you can file an issue in github and assign it to a person whose account was used to set up the two flows. 
This action will trigger the flow and will talk to YunnyAlfredBot. 

If YunnyAlfredBot works properly, you will see the following behaviors. 
* For a new issue: 
![Image of YunnyAlfredBot](/ReadMeMaterials/YunnyAlfredBot_sample.PNG)

* When a comment is made:
![Image of YunnyAlfredBot](/ReadMeMaterials/newcommentismade.PNG)

* When the issue is closed:
![Image of YunnyAlfredBot](/ReadMeMaterials/issueisclosed.PNG)

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