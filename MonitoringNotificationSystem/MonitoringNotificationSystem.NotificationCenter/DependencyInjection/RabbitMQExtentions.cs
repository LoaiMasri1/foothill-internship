using MonitoringNotificationSystem.MessageBroker;

namespace MonitoringNotificationSystem.NotificationProcessor.DependencyInjection;

public static class RabbitMQExtentions
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker>(
            new RabbitMQMessageBroker(EnviromentVeriables.rabbitMQConnectionString)
        );

        return services;
    }
}
