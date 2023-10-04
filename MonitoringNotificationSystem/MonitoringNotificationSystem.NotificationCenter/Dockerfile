#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MonitoringNotificationSystem.NotificationCenter/MonitoringNotificationSystem.NotificationCenter.csproj", "MonitoringNotificationSystem.NotificationCenter/"]
COPY ["MonitoringNotificationSystem.MessageBroker/MonitoringNotificationSystem.MessageBroker.csproj", "MonitoringNotificationSystem.MessageBroker/"]
COPY ["MonitoringNotificationSystem.Shared/MonitoringNotificationSystem.Shared.csproj", "MonitoringNotificationSystem.Shared/"]
RUN dotnet restore "MonitoringNotificationSystem.NotificationCenter/MonitoringNotificationSystem.NotificationCenter.csproj"
COPY . .
WORKDIR "/src/MonitoringNotificationSystem.NotificationCenter"
RUN dotnet build "MonitoringNotificationSystem.NotificationCenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonitoringNotificationSystem.NotificationCenter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonitoringNotificationSystem.NotificationCenter.dll"]