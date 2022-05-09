# dotnet-signalr-rosetta-stone
[![Build Status](https://beckshome.visualstudio.com/dotnet-signalr-rosetta-stone/_apis/build/status/thbst16.dotnet-signalr-rosetta-stone?branchName=main)](https://beckshome.visualstudio.com/dotnet-signalr-rosetta-stone/_build/latest?definitionId=12&branchName=main)
![Docker Image Version (latest by date)](https://img.shields.io/docker/v/thbst16/dotnet-signalr-rosetta-stone?logo=docker)
![Uptime Robot ratio (7 days)](https://img.shields.io/uptimerobot/ratio/7/m791677464-e32bac199b783e7303fdc5bc?logo=http)

The Dotnet SignalR Rosetta Stone application (aka [Rosetta Stone Chat](https://dotnet-signalr-rosetta-stone.azurewebsites.net/)) is a demo application built with the [Blazor](https://blazor.net) framework using server hosting. The application uses SignalR to support a real time chat user interface with calls to Azure Cognitive Translator services to perform real time translation across Chinese, English, German, Russian and Spanish languages.

# Notional Architecture

# Screens

### Login form
![Rosetta Stone Chat Login Form](https://s3.amazonaws.com/s3.beckshome.com/20220508-dotnet-signalr-rosetta-stone-chat-login.jpg)
### In Progress Chat - English
![Rosetta Stone Chat - English](https://s3.amazonaws.com/s3.beckshome.com/20220508-dotnet-signalr-rosetta-stone-chat-english.jpg)
### In Progress Chat - German
![Rosetta Stone Chat - German](https://s3.amazonaws.com/s3.beckshome.com/20220508-dotnet-signalr-rosetta-stone-chat-german.jpg)
### In Progress Chat - Russian
![Rosetta Stone Chat - Russian](https://s3.amazonaws.com/s3.beckshome.com/20220508-dotnet-signalr-rosetta-stone-chat-russian.jpg)

# Impact and Future

The Rosetta Stone Chat application provides the building blocks for multi-lingual, real-time chat. There are backlog work items for increasing configurability, supporting additional languages and mobile compatability. The most pressing modification is the internationalization and globalization of the base user interface, including the support of right-to-left languages, as appropriate.

# Motivation and Credits

Two articles got me 90% of where I needed to go with the initial Rosetta Stone Chat application. The Blazor Server chat tutorial provides excellent scaffolding for getting SignalR interactions up and running with Blazor. Jeeva Subburaj's article on building a real time translation chat using SignalR and Azure Cognitive services bridged many of the gaps from the base tutorial. Since it's pre-Blazor, there was still some work to be done to get this where it needed to be.

* [Tutorial: Build a Blazor Server Chat App](https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-tutorial-build-blazor-server-chat-app)
* [Realtime Language Translation Chat using SignalR and Azure Cognitive Services](https://jeevasubburaj.com/2018/06/06/real-time-language-translation-chat/)