namespace MonitoringNotificationSystem.NotificationProcessor;

public class EnviromentVeriables
{
    public static readonly string mongoUrl = Environment.GetEnvironmentVariable("MONGO_URL")!;
    public static readonly string mongoDB = Environment.GetEnvironmentVariable("MONGO_DB")!;
    private static readonly string rabbitMQUser = Environment.GetEnvironmentVariable(
        "RABBITMQ_USER"
    )!;
    private static readonly string rabbitMQPassword = Environment.GetEnvironmentVariable(
        "RABBITMQ_PASSWORD"
    )!;
    private static readonly string rabbitMQHost = Environment.GetEnvironmentVariable(
        "RABBITMQ_HOST"
    )!;

    public static readonly string rabbitMQConnectionString =
        $"amqp://{rabbitMQUser}:{rabbitMQPassword}@{rabbitMQHost}";

    public static readonly double MemoryUsageAnomalyThresholdPercentage = double.Parse(
        Environment.GetEnvironmentVariable("MEMORY_USAGE_ANOMALY_THRESHOLD_PERCENTAGE")!
    );
    public static readonly double CpuUsageAnomalyThresholdPercentage = double.Parse(
        Environment.GetEnvironmentVariable("CPU_USAGE_ANOMALY_THRESHOLD_PERCENTAGE")!
    );
    public static readonly double MemoryUsageThresholdPercentage = double.Parse(
        Environment.GetEnvironmentVariable("MEMORY_USAGE_THRESHOLD_PERCENTAGE")!
    );
    public static readonly double CpuUsageThresholdPercentage = double.Parse(
        Environment.GetEnvironmentVariable("CPU_USAGE_THRESHOLD_PERCENTAGE")!
    );

    public static readonly string ServerIdentifier = Environment.GetEnvironmentVariable(
        "SERVER_IDENTIFIER"
    )!;
    public static readonly int SamplingIntervalSeconds = int.Parse(
        Environment.GetEnvironmentVariable("SAMPLING_INTERVAL_SECONDS")!
    );
}
