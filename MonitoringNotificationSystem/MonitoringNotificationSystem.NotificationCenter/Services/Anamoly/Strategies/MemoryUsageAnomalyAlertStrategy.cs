using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services.Anamoly.Strategies;

public class MemoryUsageAnomalyAlertStrategy : IAnomalyAlertStrategy
{
    public bool IsAlert(ServerStatistics statistics, ServerStatistics previousStatistics) =>
        Utilities.Anomaly.CalculatePercentageChange(
            previousStatistics.MemoryUsage,
            statistics.MemoryUsage
        ) > EnviromentVeriables.MemoryUsageAnomalyThresholdPercentage;

    public string GetAlertMessage() => "Anomaly Alert: Sudden increase in Memory Usage!";
}
