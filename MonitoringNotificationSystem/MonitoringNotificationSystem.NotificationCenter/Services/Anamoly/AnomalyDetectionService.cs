using Microsoft.AspNetCore.SignalR;
using MonitoringNotificationSystem.NotificationCenter.Hubs;
using MonitoringNotificationSystem.NotificationCenter.Repositories;
using MonitoringNotificationSystem.NotificationCenter.Services.Strategies;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services;

public class AnomalyDetectionService : IAnomalyDetectionService
{
    private readonly IHubContext<NotificationHub, IStatisticsClient> _hub;
    private readonly List<IAnomalyAlertStrategy> _alertStrategies;
    private readonly INotificationRepository _notificationRepository;

    public AnomalyDetectionService(
        IHubContext<NotificationHub, IStatisticsClient> hub,
        List<IAnomalyAlertStrategy> alertStrategies,
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
