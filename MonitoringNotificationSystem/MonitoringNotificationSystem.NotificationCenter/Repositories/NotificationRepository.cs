using MongoDB.Driver;
using MonitoringNotificationSystem.Shared.Data;

namespace MonitoringNotificationSystem.NotificationCenter.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<ServerStatistics> _collection;
    private const string collectionName = "Statistics";

    public NotificationRepository()
    {
        var client = new MongoClient(EnviromentVeriables.mongoUrl);
        _database = client.GetDatabase(EnviromentVeriables.mongoDB);
        _collection = _database.GetCollection<ServerStatistics>(collectionName);
    }

    public async Task SaveStatisticsAsync(ServerStatistics statistics) =>
        await _collection.InsertOneAsync(statistics);
}
