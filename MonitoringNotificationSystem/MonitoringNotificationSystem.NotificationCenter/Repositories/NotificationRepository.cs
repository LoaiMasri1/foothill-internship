using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MonitoringNotificationSystem.Shared.Configurations;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<ServerStatistics> _collection;
    private const string collectionName = "Statistics";

    public NotificationRepository(IOptions<MongoDBConfig> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _database = client.GetDatabase(options.Value.DatabaseName);
        _collection = _database.GetCollection<ServerStatistics>(collectionName);
    }

    public async Task SaveStatisticsAsync(ServerStatistics statistics) =>
        await _collection.InsertOneAsync(statistics);
}
