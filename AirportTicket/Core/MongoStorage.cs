using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AirportTicket.Core;

public class MongoStorage : IStorage
{

    private static readonly IConfiguration configuration = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .Build();


    private readonly IMongoDatabase _database;

    private static IStorage? _instance;
    public static IStorage Instance => _instance ??= 
        new MongoStorage(configuration.
            GetSection("MongoDB:ConnectionString").Value!, 
            configuration.GetSection("MongoDB:DatabaseName").Value!);

    private MongoStorage(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public async Task<ICollection<T>> ReadAsync<T>() where T : class
    {
        var collectionName = typeof(T).Name;
        var collection = _database.GetCollection<T>(collectionName);
        var documents = await collection.FindAsync(Builders<T>.Filter.Empty);

        return await documents.ToListAsync();
    }

    public async Task WriteAsync<T>(ICollection<T> data) where T : class
    {
        var collectionName = typeof(T).Name;
        var collection = _database.GetCollection<T>(collectionName);
        await collection.InsertManyAsync(data);
    }
}
