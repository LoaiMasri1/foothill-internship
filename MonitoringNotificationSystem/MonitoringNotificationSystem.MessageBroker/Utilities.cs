using Polly.Retry;
using Polly;
using RabbitMQ.Client;

namespace MonitoringNotificationSystem.MessageBroker;

public static class Utilities
{
    public static partial class Polly
    {
        public static RetryPolicy CreateRetryPolicy<TException>(
            int maxRetryCount,
            TimeSpan delayBetweenRetries,
            Action<Exception, TimeSpan> onRetry = null!
        )
            where TException : Exception
        {
            var retryCount = 0;

            return Policy
                .Handle<TException>()
                .WaitAndRetry(
                    retryCount: maxRetryCount,
                    sleepDurationProvider: attempt => delayBetweenRetries,
                    onRetry: onRetry
                        ?? (
                            (exception, timeSpan) =>
                            {
                                retryCount++;
                                Console.WriteLine(
                                    $"""
                                Retry {retryCount} of {maxRetryCount}.
                                Retrying in {timeSpan.TotalSeconds} seconds.
                                Because: {exception.Message}
                                """
                                );
                            }
                        )
                );
        }
    }

    public static partial class RabbitMQ
    {
        public static IConnection CreateConnection(string connectionString)
        {
            var factory = new ConnectionFactory { Uri = new Uri(connectionString) };
            return factory.CreateConnection();
        }
    }
}
