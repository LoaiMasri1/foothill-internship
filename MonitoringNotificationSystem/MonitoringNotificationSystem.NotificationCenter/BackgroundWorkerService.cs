using Microsoft.Extensions.Options;
using MonitoringNotificationSystem.Shared.Configurations;

namespace MonitoringNotificationSystem.NotificationCenter;

public class BackgroundWorkerService : BackgroundService
{
    private readonly Connector _connector;
    private readonly ILogger<BackgroundWorkerService> _logger;
    private readonly ServerStatisticsConfig _config;

    public BackgroundWorkerService(
        Connector connector,
        ILogger<BackgroundWorkerService> logger,
        IOptions<ServerStatisticsConfig> options
    )
    {
        _connector = connector;
        _logger = logger;
        _config = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await _connector.StartAsync();

            var delay = TimeSpan.FromSeconds(_config.SamplingIntervalSeconds);

            await Task.Delay(delay, stoppingToken);
        }
    }
}
