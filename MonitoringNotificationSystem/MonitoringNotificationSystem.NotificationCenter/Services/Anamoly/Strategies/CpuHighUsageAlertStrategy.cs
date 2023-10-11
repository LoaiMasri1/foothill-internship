using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Services.Anamoly.Strategies;

public class CpuHighUsageAlertStrategy : IAnomalyAlertStrategy
{
    public bool IsAlert(ServerStatistics statistics, ServerStatistics p) =>
        statistics.CpuUsage > EnviromentVeriables.CpuUsageThresholdPercentage;

    public string GetAlertMessage() => "High Usage Alert: CPU Usage exceeded threshold!";
}
