using MonitoringNotificationSystem.NotificationProcessor.DependencyInjection;
using MonitoringNotificationSystem.NotificationProcessor.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddDependencies();

var app = builder.Build();

app.MapHub<NotificationHub>("/notification-hub");

app.Run();
