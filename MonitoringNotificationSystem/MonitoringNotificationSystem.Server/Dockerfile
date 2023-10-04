#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MonitoringNotificationSystem.Server/MonitoringNotificationSystem.Server.csproj", "MonitoringNotificationSystem.Server/"]
COPY ["MonitoringNotificationSystem.MessageBroker/MonitoringNotificationSystem.MessageBroker.csproj", "MonitoringNotificationSystem.MessageBroker/"]
COPY ["MonitoringNotificationSystem.Shared/MonitoringNotificationSystem.Shared.csproj", "MonitoringNotificationSystem.Shared/"]
RUN dotnet restore "MonitoringNotificationSystem.Server/MonitoringNotificationSystem.Server.csproj"
COPY . .
WORKDIR "/src/MonitoringNotificationSystem.Server"
RUN dotnet build "MonitoringNotificationSystem.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonitoringNotificationSystem.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonitoringNotificationSystem.Server.dll"]