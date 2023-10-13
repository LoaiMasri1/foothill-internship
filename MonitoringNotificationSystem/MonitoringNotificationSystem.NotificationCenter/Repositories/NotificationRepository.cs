using MongoDB.Driver;

namespace MonitoringNotificationSystem.NotificationProcessor.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<MongoServerStatistics> _collection;

    public NotificationRepository(IMongoCollection<MongoServerStatistics> collection) =>
        _collection = collection;

    public async Task SaveStatisticsAsync(MongoServerStatistics statistics) =>
        await _collection.InsertOneAsync(statistics);

    public async Task<MongoServerStatistics> GetLastAsync() =>
        await _collection
            .Find(FilterDefinition<MongoServerStatistics>.Empty)
            .SortByDescending(s => s.Timestamp)
            .FirstOrDefaultAsync();
}
