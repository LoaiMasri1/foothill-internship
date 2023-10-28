using MonitoringNotificationSystem.MessageBroker;
using MonitoringNotificationSystem.Server.Readers;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.Server;

public class ServerStatisticsCollector
{
    private readonly string _serverIdentifier;
    private readonly IMessageBroker _messageBroker;

    public ServerStatisticsCollector(string serverIdentifier, IMessageBroker messageBroker)
    {
        _serverIdentifier = serverIdentifier;
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

    public async Task PublishServerStatisticsAsync()
    {
        var state = CollectStatistics();

        await Console.Out.WriteLineAsync("Memory usage: " + state.MemoryUsage + " MB");

        _messageBroker.Publish($"ServerStatistics.{_serverIdentifier}", state);
    }
}
