using AirportTicket.Common.Models;
using AirportTicket.Common.Wrappers;
using System.Text.Json;

namespace AirportTicket.Core;

public class Storage : IStorage
{
    private Storage(IFileWrapper fileWrapper)
    {
        _fileWrapper = fileWrapper;
    }


    private static IStorage? _instance;
    private readonly IFileWrapper _fileWrapper;

    public static IStorage GetInstance(IFileWrapper fileWrapper = null)
    {
        fileWrapper ??= new FileWrapper();

        _instance ??= new Storage(fileWrapper);

        return _instance;
    }


    public async Task<ICollection<T>> ReadAsync<T>() where T : class
    {
        var collectionName = typeof(T).Name;
        var filePath = GetFilePath(collectionName);
        await CreateDirectoryIfFileNotExist(filePath);

        return await ReadCollectionFromFileAsync<T>(filePath);
    }

    public async Task WriteAsync<T>(ICollection<T> data) where T : class
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

            await _fileWrapper.WriteAllTextAsync(filePath, json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message, e.StackTrace);
        }
    }

    private async Task CreateDirectoryIfFileNotExist(string filePath)
    {
        var isExists = _fileWrapper.Exists(filePath);

        if (!isExists)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await File.WriteAllTextAsync(filePath, "{}");
        }
    }

    private async Task<ICollection<T>> ReadCollectionFromFileAsync<T>
        (string filePath) where T : class
    {
        var file = await _fileWrapper.ReadAllTextAsync(filePath);
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
