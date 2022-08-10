FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
COPY src/Api/*.csproj ./
RUN dotnet restore
    
COPY src/Api ./
RUN dotnet publish -c Release -o out
    
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /www/app

RUN apt update -y && apt upgrade -y
RUN apt install -y build-essential

RUN groupadd -g 1000 www
RUN useradd -u 1000 -ms /bin/bash -g www www 
USER www

COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Api.dll"]