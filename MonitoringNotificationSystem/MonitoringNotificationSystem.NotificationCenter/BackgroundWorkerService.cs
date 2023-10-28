namespace MonitoringNotificationSystem.NotificationProcessor;

public class BackgroundWorkerService : BackgroundService
{
    private readonly IConnector _connector;
    private readonly ILogger<BackgroundWorkerService> _logger;

    public BackgroundWorkerService(IConnector connector, ILogger<BackgroundWorkerService> logger) =>
        (_connector, _logger) = (connector, logger);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await _connector.StartAsync();
    }
}
