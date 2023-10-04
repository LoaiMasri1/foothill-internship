using MonitoringNotificationSystem.NotificationCenter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<NotificationHub>("/notification-hub");

app.Run();
