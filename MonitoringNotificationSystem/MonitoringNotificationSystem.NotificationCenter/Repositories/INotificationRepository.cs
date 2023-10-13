using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationProcessor.Repositories;

public interface INotificationRepository
{
    Task SaveStatisticsAsync(MongoServerStatistics statistics);
    Task<MongoServerStatistics> GetLastAsync();
}

public class MongoServerStatistics : ServerStatistics
{
    [BsonId]
    public ObjectId Id { get; set; }
}
