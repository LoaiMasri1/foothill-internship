using Microsoft.AspNetCore.SignalR;

namespace MonitoringNotificationSystem.NotificationProcessor.Hubs;

public class NotificationHub : Hub<IStatisticsClient>
{
    public async Task SendMessage(string message) => await Clients.All.ReceiveMessage(message);

    public async Task SendAnomalyMessage(string message) =>
        await Clients.All.AnomalyMessage(message);
}
