using Microsoft.AspNetCore.SignalR;
using MonitoringNotificationSystem.MessageBroker;
using MonitoringNotificationSystem.NotificationCenter.Hubs;
using MonitoringNotificationSystem.NotificationCenter.Repositories;
using MonitoringNotificationSystem.NotificationCenter.Services.Anamoly;
using MonitoringNotificationSystem.Shared.Data;
using System.Text.Json;

namespace MonitoringNotificationSystem.NotificationCenter;

public class Connector
{
    private readonly IHubContext<NotificationHub, IStatisticsClient> _hub;
    private readonly ILogger<Connector> _logger;
    private readonly IMessageBroker _broker;
    private readonly INotificationRepository _notificationRepository;
    private readonly IAnomalyDetectionService _anomalyDetectionService;

    public Connector(
        IHubContext<NotificationHub, IStatisticsClient> hubContext,
        ILogger<Connector> logger,
        INotificationRepository notificationRepository,
        IAnomalyDetectionService anomalyDetectionService,
        IMessageBroker broker
    )
    {
        _hub = hubContext;
        _logger = logger;
        _broker = broker;
        _notificationRepository = notificationRepository;
        _anomalyDetectionService = anomalyDetectionService;
    }

    public async Task StartAsync()
    {
        var topic = $"ServerStatistics.{EnviromentVeriables.ServerIdentifier}";
        _logger.LogInformation("Starting connector {}", topic);
        try
        {
            await _broker.SubscribeAsync<ServerStatistics>(topic, ProcessServerStatisticsAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting statistics");
        }
    }

    private async Task ProcessServerStatisticsAsync(ServerStatistics statistics)
    {
        _logger.LogInformation(
            "Sending statistics to clients {}",
            EnviromentVeriables.SamplingIntervalSeconds
        );
        await _hub.Clients.All.ReceiveMessage(JsonSerializer.Serialize(statistics));

        var mongoServerStatistics = new MongoServerStatistics
        {
            Timestamp = statistics.Timestamp,
            AvailableMemory = statistics.AvailableMemory,
            CpuUsage = statistics.CpuUsage,
            MemoryUsage = statistics.MemoryUsage,
        };

        await _notificationRepository.SaveStatisticsAsync(mongoServerStatistics);
        await _anomalyDetectionService.CheckAndSendAnomalyAlertsAsync(statistics);
    }
}
