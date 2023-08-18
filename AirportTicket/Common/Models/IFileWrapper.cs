namespace AirportTicket.Common.Models;

public interface IFileWrapper
{
    bool Exists(string path);
    Task<string> ReadAllTextAsync(string path);
    Task WriteAllTextAsync(string path, string contents);
}
