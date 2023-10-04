using MonitoringNotificationSystem.NotificationCenter;
using MonitoringNotificationSystem.NotificationCenter.Hubs;
using MonitoringNotificationSystem.NotificationCenter.Repositories;
using MonitoringNotificationSystem.NotificationCenter.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<IAnomalyDetectionService, AnomalyDetectionService>();

builder.Services.AddSingleton<Connector>();

builder.Services.AddHostedService<BackgroundWorkerService>();

var app = builder.Build();

app.MapHub<NotificationHub>("/notification-hub");

app.Run();
