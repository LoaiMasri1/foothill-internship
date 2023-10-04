using Microsoft.AspNetCore.SignalR;

namespace MonitoringNotificationSystem.NotificationCenter;
public class NotificationHub : Hub
{
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}
