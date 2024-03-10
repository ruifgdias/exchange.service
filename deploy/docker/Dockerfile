# base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["exchange.service/Infrastructure.CrossCutting/Infrastructure.CrossCutting.csproj", "./Infrastructure.CrossCutting/"]
RUN dotnet restore "./Infrastructure.CrossCutting/Infrastructure.CrossCutting.csproj"
COPY ["exchange.service/Infrastructure.CrossCutting/", "./Infrastructure.CrossCutting/"]

COPY ["exchange.service/ExchangeCurrencyRateEventPublisher/ExchangeCurrencyRateEventPublisher.csproj", "./ExchangeCurrencyRateEventPublisher/"]
RUN dotnet restore "./ExchangeCurrencyRateEventPublisher/ExchangeCurrencyRateEventPublisher.csproj"
COPY ["exchange.service/ExchangeCurrencyRateEventPublisher/", "./ExchangeCurrencyRateEventPublisher/"]

COPY ["exchange.service/ExchangeRate.ExternalClient/ExchangeRate.ExternalClient.csproj", "./ExchangeRate.ExternalClient/"]
RUN dotnet restore "./ExchangeRate.ExternalClient/ExchangeRate.ExternalClient.csproj"
COPY ["exchange.service/ExchangeRate.ExternalClient/", "./ExchangeRate.ExternalClient/"]

COPY ["exchange.service/Infrastructure.Persistence/Infrastructure.Persistence.csproj", "./Infrastructure.Persistence/"]
RUN dotnet restore "./Infrastructure.Persistence/Infrastructure.Persistence.csproj"
COPY ["exchange.service/Infrastructure.Persistence/", "./Infrastructure.Persistence/"]

COPY ["exchange.service/ExchangeData/ExchangeData.csproj", "./ExchangeData/"]
RUN dotnet restore "./ExchangeData/ExchangeData.csproj"
COPY ["exchange.service/ExchangeData/", "./ExchangeData/"]

COPY ["exchange.service/ExchangeCore/ExchangeCore.csproj", "./ExchangeCore/"]
RUN dotnet restore "./ExchangeCore/ExchangeCore.csproj"
COPY ["exchange.service/ExchangeCore/", "./ExchangeCore/"]

COPY ["exchange.service/ExchangeApi/ExchangeApi.csproj", "./ExchangeApi/"]
RUN dotnet restore "./ExchangeApi/ExchangeApi.csproj"
COPY ["exchange.service/ExchangeApi/", "./ExchangeApi/"]

WORKDIR "/src/."
#RUN dotnet build "/ExchangeCore/ExchangeCore.csproj" -c Release -o /app/build
RUN dotnet build "./ExchangeApi/ExchangeApi.csproj" -c Release -o /app/build

# publish image
FROM build AS publish
RUN dotnet publish "./ExchangeApi/ExchangeApi.csproj" -c Release -o /app/publish

# final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangeApi.dll"]