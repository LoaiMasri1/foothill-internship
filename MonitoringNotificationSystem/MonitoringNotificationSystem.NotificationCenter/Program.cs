using MonitoringNotificationSystem.NotificationCenter.Hubs;
using MonitoringNotificationSystem.NotificationCenter.Setups;
using MonitoringNotificationSystem.Shared.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.Configure<ServerStatisticsConfig>(
    builder.Configuration.GetSection(nameof(ServerStatisticsConfig))
);

builder.Services.Configure<AnomalyDetectionConfig>(
    builder.Configuration.GetSection(nameof(AnomalyDetectionConfig))
);

builder.Services.ConfigureOptions<MongoDBConfigSetup>();

var app = builder.Build();

app.MapHub<NotificationHub>("/notification-hub");

app.Run();
