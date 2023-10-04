using MonitoringNotificationSystem.NotificationCenter;
using MonitoringNotificationSystem.NotificationCenter.Hubs;
using MonitoringNotificationSystem.NotificationCenter.Repositories;
using MonitoringNotificationSystem.NotificationCenter.Services;
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

builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<IAnomalyDetectionService, AnomalyDetectionService>();

builder.Services.AddSingleton<Connector>();

var app = builder.Build();

app.MapHub<NotificationHub>("/notification-hub");

app.Run();
