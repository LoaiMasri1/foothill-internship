using System.Text.Json;

namespace AirportTicket.Core;

public class Storage
{
    private Storage() { }

    public static async Task<ICollection<T>> ReadAsync<T>() where T : class
    {
        var collectionName = typeof(T).Name;
        var filePath = GetFilePath(collectionName);
        await CreateDircetoryIfFileNotExist(filePath);

        return await ReadCollectionFromFileAsync<T>(filePath);
    }

    private static async Task CreateDircetoryIfFileNotExist(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await File.WriteAllTextAsync(filePath, "{}");
        }
    }

    public static async Task WriteAsync<T>(ICollection<T> data) where T : class
    {
        var collectionName = typeof(T).Name;
        string filePath = GetFilePath(collectionName);

        try
        {
            var result = new Dictionary<string, List<T>>
                {
                    { collectionName, data.ToList() }
                };

            var json = JsonSerializer.Serialize(result);

            await File.WriteAllTextAsync(filePath, json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message, e.StackTrace);
        }
    }

    private static async Task<ICollection<T>> ReadCollectionFromFileAsync<T>
        (string filePath) where T : class
    {
        var file = await File.ReadAllTextAsync(filePath);
        var result = JsonSerializer.Deserialize<Dictionary<string, List<T>>>(file);

        if (result == null || !result.TryGetValue(typeof(T).Name, out var collection))
        {
            return new List<T>();
        }

        return collection;
    }

    private static string GetFilePath(string collectionName)
    {
        var documentsFolderPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        return Path.Combine(documentsFolderPath, "Storage", $"{collectionName}.json");
    }
}
