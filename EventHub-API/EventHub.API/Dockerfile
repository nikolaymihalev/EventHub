FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY EventHub.API/*.csproj ./EventHub.API/
COPY EventHub.Core/*.csproj ./EventHub.Core/
COPY EventHub.Infrastructure/*.csproj ./EventHub.Infrastructure/

RUN dotnet restore EventHub.API/EventHub.API.csproj

COPY . .

RUN dotnet publish EventHub.API/EventHub.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 7006

ENTRYPOINT ["dotnet", "EventHub.API.dll"]