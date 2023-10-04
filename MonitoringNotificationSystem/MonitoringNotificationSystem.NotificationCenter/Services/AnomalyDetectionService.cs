using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using MonitoringNotificationSystem.NotificationCenter.Hubs;
using MonitoringNotificationSystem.Shared.Configurations;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services;

public class AnomalyDetectionService : IAnomalyDetectionService
{
    private static ServerStatistics _previousStatistics = new();
    private static AnomalyDetectionConfig _anomalyDetectionConfig = new();
    private readonly IHubContext<NotificationHub, IStatisticsClient> _hub;

    public AnomalyDetectionService(
        IOptions<AnomalyDetectionConfig> anomalyDetectionConfig,
        IHubContext<NotificationHub, IStatisticsClient> hub
    )
    {
        _anomalyDetectionConfig = anomalyDetectionConfig.Value;
        _hub = hub;
    }

    public void CheckAndSendAnomalyAlerts(ServerStatistics statistics)
    {
        if (IsMemoryHighUsage(statistics))
            SendMemoryHighUsageAlert();

        if (IsCpuHighUsage(statistics))
            SendCpuHighUsageAlert();

        if (IsMemoryUsageAnomaly(statistics))
            SendMemoryUsageAnomalyAlert();

        if (IsCpuUsageAnomaly(statistics))
            SendCpuUsageAnomalyAlert();

        _previousStatistics = statistics;
    }

    private void SendCpuUsageAnomalyAlert() =>
        _hub.Clients.All.AnomalyMessage("Anomaly Alert: Sudden increase in CPU Usage!");

    private void SendMemoryUsageAnomalyAlert() =>
        _hub.Clients.All.AnomalyMessage("Anomaly Alert: Sudden increase in Memory Usage!");

    private void SendCpuHighUsageAlert() =>
        _hub.Clients.All.AnomalyMessage("High Usage Alert: CPU Usage exceeded threshold!");

    private void SendMemoryHighUsageAlert() =>
        _hub.Clients.All.AnomalyMessage("High Usage Alert: Memory Usage exceeded threshold!");

    private static bool IsMemoryUsageAnomaly(ServerStatistics statistics) =>
        CalculatePercentageChange(_previousStatistics.MemoryUsage, statistics.MemoryUsage)
        > _anomalyDetectionConfig.MemoryUsageAnomalyThresholdPercentage;

    private static bool IsCpuUsageAnomaly(ServerStatistics statistics) =>
        CalculatePercentageChange(_previousStatistics.CpuUsage, statistics.CpuUsage)
        > _anomalyDetectionConfig.CpuUsageAnomalyThresholdPercentage;

    private static bool IsMemoryHighUsage(ServerStatistics statistics)
    {
        double memoryUsagePercentage =
            statistics.MemoryUsage / (statistics.MemoryUsage + statistics.AvailableMemory);
        return memoryUsagePercentage > _anomalyDetectionConfig.MemoryUsageThresholdPercentage;
    }

    private static bool IsCpuHighUsage(ServerStatistics statistics) =>
        statistics.CpuUsage > _anomalyDetectionConfig.CpuUsageThresholdPercentage;

    private static double CalculatePercentageChange(double previousValue, double currentValue)
    {
        if (previousValue == 0)
            return 0;

        return (currentValue - previousValue) / previousValue;
    }
}
