using CsvHelper.Configuration;
namespace AirportTicket.Common.Models;
public interface ICSVReader
{
    IEnumerable<TResult> Read<TResult, TMap>(string filePath) where TResult : class where TMap : ClassMap;
}
