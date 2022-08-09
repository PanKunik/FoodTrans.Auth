FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
COPY src/FoodTrans.Auth/*.csproj ./
RUN dotnet restore
    
COPY src/FoodTrans.Auth ./
RUN dotnet publish -c Release -o out
    
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "FoodTrans.Auth.dll"]

