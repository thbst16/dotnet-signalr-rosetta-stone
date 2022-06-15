# dotnet-signalr-rosetta-stone
[![Build Status](https://beckshome.visualstudio.com/dotnet-signalr-rosetta-stone/_apis/build/status/thbst16.dotnet-signalr-rosetta-stone?branchName=main)](https://beckshome.visualstudio.com/dotnet-signalr-rosetta-stone/_build/latest?definitionId=12&branchName=main)
![Docker Image Version (latest by date)](https://img.shields.io/docker/v/thbst16/dotnet-signalr-rosetta-stone?logo=docker)
![Uptime Robot ratio (7 days)](https://img.shields.io/uptimerobot/ratio/7/m791677464-e32bac199b783e7303fdc5bc?logo=http)

The Dotnet SignalR Rosetta Stone application (aka [Rosetta Stone Chat](https://dotnet-signalr-rosetta-stone.azurewebsites.net/)) is a demo application built with the [Blazor](https://blazor.net) framework using server hosting. The application uses SignalR to support a real time chat user interface with calls to Azure Cognitive Translator services to perform real time translation across Chinese, English, German, Russian and Spanish languages.

# Notional Architecture
The Rosetta Stone Chat application consists of 2 core components, as illustrated in the diagram below:

1. The Blazor application running SignalR server is hosted in a docker container, providing real-time websocket based chat services with front end users. These users interact with the application in their language of choice, with the application facilitating all translation services. In a production environment, these services would be hosted in the Azure SignalR Hub managed service to provide the necessary scalability.

2. The Azure service layer provides the translation services to translate user messages from their language into the 5 supported languages of the application: Chinese, English, German, Russian and Spanish. To avoid overusing these services, these calls are currently capped at 2,000 API invocations for the life of the server process.

![Notional Architecture](https://s3.amazonaws.com/s3.beckshome.com/20220509-dotnet-signalr-rosetta-stone-architecturejpg.jpg)

# Screens

### Login form
![Rosetta Stone Chat Login Form](https://s3.amazonaws.com/s3.beckshome.com/20220516-dotnet-signalr-rosetta-stone-chat-login.jpg)
### In Progress Chat - English
![Rosetta Stone Chat - English](https://s3.amazonaws.com/s3.beckshome.com/20220516-dotnet-signalr-rosetta-stone-chat-english.jpg)
### In Progress Chat - German
![Rosetta Stone Chat - German](https://s3.amazonaws.com/s3.beckshome.com/20220516-dotnet-signalr-rosetta-stone-chat-german.jpg)
### In Progress Chat - Russian
![Rosetta Stone Chat - Russian](https://s3.amazonaws.com/s3.beckshome.com/20220516-dotnet-signalr-rosetta-stone-chat-russian.jpg)

# Impact and Future

The Rosetta Stone Chat application provides the building blocks for multi-lingual, real-time chat. There are backlog work items for increasing configurability, supporting additional languages and mobile compatability.

# Motivation and Credits

Two articles got me 90% of where I needed to go with the initial Rosetta Stone Chat application. The Blazor Server chat tutorial provides excellent scaffolding for getting SignalR interactions up and running with Blazor. Jeeva Subburaj's article on building a real time translation chat using SignalR and Azure Cognitive services bridged many of the gaps from the base tutorial. Since it's pre-Blazor, there was still some work to be done to get this where it needed to be.

* [Tutorial: Build a Blazor Server Chat App](https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-tutorial-build-blazor-server-chat-app)
* [Realtime Language Translation Chat using SignalR and Azure Cognitive Services](https://jeevasubburaj.com/2018/06/06/real-time-language-translation-chat/)
