using Microsoft.AspNetCore.SignalR;
using MonitoringNotificationSystem.NotificationProcessor.Hubs;
using MonitoringNotificationSystem.NotificationProcessor.Repositories;
using MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly.Strategies;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationProcessor.Services.Anamoly;

public class AnomalyDetectionService : IAnomalyDetectionService
{
    private readonly IHubContext<NotificationHub, IStatisticsClient> _hub;
    private readonly IEnumerable<IAnomalyAlertStrategy> _alertStrategies;
    private readonly INotificationRepository _notificationRepository;

    public AnomalyDetectionService(
        IHubContext<NotificationHub, IStatisticsClient> hub,
        IEnumerable<IAnomalyAlertStrategy> alertStrategies,
        INotificationRepository notificationRepository
    )
    {
        _hub = hub;
        _alertStrategies = alertStrategies;
        _notificationRepository = notificationRepository;
    }

    public async Task CheckAndSendAnomalyAlertsAsync(ServerStatistics statistics)
    {
        var previousStatistics = await _notificationRepository.GetLastAsync() ?? statistics;

        foreach (IAnomalyAlertStrategy alertStrategy in _alertStrategies)
        {
            if (alertStrategy.IsAlert(statistics, previousStatistics))
                await SendAlertAsync(alertStrategy.GetAlertMessage());
        }
    }

    public async Task SendAlertAsync(string message) =>
        await _hub.Clients.All.AnomalyMessage(message);
}
