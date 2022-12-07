FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
COPY ["./src/FoodTrans.Auth.Domain/Domain.csproj", "src/Domain/"]
COPY ["./src/FoodTrans.Auth.Application/Application.csproj", "src/Application/"]
COPY ["./src/FoodTrans.Auth.Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["./src/FoodTrans.Auth.Api/Api.csproj", "src/Api/"]

RUN dotnet restore "src/Api/Api.csproj"
    
COPY . .

WORKDIR "/app/src/FoodTrans.Auth.Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build-env AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish
    
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80

RUN apt update -y && apt upgrade -y
RUN apt install -y build-essential

RUN groupadd -g 1000 www
RUN useradd -u 1000 -ms /bin/bash -g www www 
USER www

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]