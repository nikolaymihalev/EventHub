FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventHub.API/EventHub.API.csproj", "EventHub.API/"]
COPY ["EventHub.Core/EventHub.Core.csproj", "EventHub.Core/"]
COPY ["EventHub.Infrastructure/EventHub.Infrastructure.csproj", "EventHub.Infrastructure/"]
COPY ["EventHub.UnitTests/EventHub.UnitTests.csproj", "EventHub.UnitTests/"]
RUN dotnet restore "./EventHub.API/./EventHub.API.csproj"
COPY . .
WORKDIR "/src/EventHub.API"
RUN dotnet build "./EventHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventHub.API.dll"]