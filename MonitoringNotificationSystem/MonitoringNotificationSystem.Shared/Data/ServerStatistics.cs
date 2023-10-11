namespace MonitoringNotificationSystem.Shared.Data;

public class ServerStatistics
{
    public double CpuUsage { get; set; }
    public double MemoryUsage { get; set; }
    public double AvailableMemory { get; set; }
    public DateTime Timestamp { get; set; }
}
