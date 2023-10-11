using MonitoringNotificationSystem.NotificationCenter.DependencyInjection;
using MonitoringNotificationSystem.NotificationCenter.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddDependencies();

var app = builder.Build();

app.MapHub<NotificationHub>("/notification-hub");

app.Run();
