namespace MonitoringNotificationSystem.NotificationCenter.DependencyInjection;

public static class ConfigureDependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddMongo().AddRepositories().AddRabbitMQ().AddServices();

        return services;
    }
}
