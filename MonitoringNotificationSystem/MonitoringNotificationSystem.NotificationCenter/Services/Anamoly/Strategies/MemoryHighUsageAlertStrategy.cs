using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services.Anamoly.Strategies;

public class MemoryHighUsageAlertStrategy : IAnomalyAlertStrategy
{
    public bool IsAlert(ServerStatistics statistics, ServerStatistics previousStatistics)
    {
        double memoryUsagePercentage =
            statistics.MemoryUsage / (statistics.MemoryUsage + statistics.AvailableMemory);
        return memoryUsagePercentage > EnviromentVeriables.MemoryUsageThresholdPercentage;
    }

    public string GetAlertMessage() => "High Usage Alert: Memory Usage exceeded threshold!";
}
