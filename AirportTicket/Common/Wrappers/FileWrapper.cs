using AirportTicket.Common.Models;

namespace AirportTicket.Common.Wrappers;

public class FileWrapper : IFileWrapper
{
    public bool Exists(string path)
    {
        var isExists = File.Exists(path);
        return isExists;
    }

    public Task<string> ReadAllTextAsync(string path)
    {
        var text = File.ReadAllTextAsync(path);
        return text;
    }

    public Task WriteAllTextAsync(string path, string contents)
    {
        var text = File.WriteAllTextAsync(path, contents);
        return text;
    }
}
