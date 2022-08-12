# FoodTrans.Auth
Simple web API for authorizing and authenticating users created with .NET, PostgreSQL with Docker support.

## Table of Contents
- [General info](#general-info)
- [Technologies](#technologies)
- [Setup](#setup)
- [Usage](#usage)

## General info
This repository is a part of something bigger, the [FoodTrans](https://github.com/brittleheart/foodtrans) project. Module presented here is responsible for everything related to authorization and authentication. It uses JWT access tokens and refresh tokens. \
We extracted the smallest possible part of our project to make this module generic. It can be used as starting point for any other application, that needs authorization and authentication.

## Technologies
Project is built with:
- [.NET](https://docs.microsoft.com/pl-pl/dotnet/) - .NET is a free, cross-platform, open source developer platform for building many different types of applications. With .NET, you can use multiple languages, editors, and libraries to build for web, mobile, desktop, games, IoT, and more.
- [PostgreSQL](https://www.postgresql.org/) - PostgreSQL is a powerful, open source object-relational database system with over 30 years of active development that has earned it a strong reputation for reliability, feature robustness, and performance. 

## Setup
There are two ways to run this project. You can run it in dockerized conatiner or localy with .NET runtime. After cloning the repository you can:

#### Run in docker
First, make sure you have docker installed. If yes, then open your terminal and go to root folder of this repository. Then you can build the dockerfile using command:

```powershell
docker build -t foodtrans.auth:latest .
```

If build succedded, you can run the image with:

```powershell
docker run -d -p 8080:80 foodtrans.auth:latest
```

Open your browser and go under the address `http://localhost:8080` to communicate with the API.

### Run locally
To run this project, you need  to have .NET SDK installed. If you don't have, go under [this link](https://dotnet.microsoft.com/en-us/download) and download appropriate SKD for your operating system.
Next, open terminal and go under root folder of this repository. Run project with command:

```powershell
dotnet run
```

## Usage
- Authenticating users
- Allowing to authorize with JWT Token

### Have you found a bug?
If you found a bug, go ahead and open issue by providing detailed description, so we could reproduce the bug and fix it.