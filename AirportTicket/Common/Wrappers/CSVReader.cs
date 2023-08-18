using AirportTicket.Common.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AirportTicket.Common.Wrappers;

public class CSVReader : ICSVReader
{
    public IEnumerable<TResult> Read<TResult, TMap>(string filePath) where TResult : class where TMap : ClassMap
    {
        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<TMap>();
            var records = csv.GetRecords<TResult>().ToList();
            return records;
        }
        catch (CsvHelperException)
        {
            throw;
        }
    }
}
