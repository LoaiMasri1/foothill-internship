using MonitoringNotificationSystem.MessageBroker;
using MonitoringNotificationSystem.Server.Readers;
using MonitoringNotificationSystem.Shared.Configurations;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.Server;

public class ServerStatisticsCollector
{
    private readonly ServerStatisticsConfig _statisticsConfig;
    private readonly IMessageBroker _messageBroker;

    public ServerStatisticsCollector(
        ServerStatisticsConfig statisticsConfig,
        IMessageBroker messageBroker
    )
    {
        _statisticsConfig = statisticsConfig;
        _messageBroker = messageBroker;
    }

    private static ServerStatistics CollectStatistics()
    {
        var cpuUsage = CpuUsageReader.GetCpuUsage();
        var memoryUsage = MemoryUsageReader.GetMemoryUsage();
        var availableMemory = MemoryUsageReader.GetAvailableMemory();

        var statistics = new ServerStatistics
        {
            CpuUsage = cpuUsage,
            MemoryUsage = memoryUsage,
            AvailableMemory = availableMemory,
            Timestamp = DateTime.UtcNow,
        };

        return statistics;
    }

    public async Task StartAsync()
    {
        await Task.Run(async () =>
        {
            while (true)
            {
                var state = CollectStatistics();

                await Console.Out.WriteLineAsync("Memory usage: " + state.MemoryUsage + " MB");

                _messageBroker.Publish(
                    $"ServerStatistics.{_statisticsConfig.ServerIdentifier}",
                    $"ServerStatistics.{_statisticsConfig.ServerIdentifier}",
                    state
                );

                var delay = TimeSpan.FromSeconds(_statisticsConfig.SamplingIntervalSeconds);

                await Task.Delay(delay);
            }
        });
    }
}
