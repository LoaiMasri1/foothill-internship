using AirportTicket.Common.Constants;
using AirportTicket.Common.Models;
using AirportTicket.Common.Services.Intf;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using static AirportTicket.Common.Constants.PathConstants;

namespace AirportTicket.Common.Services.Impl
{
    public class ImportCSVFileService<TResult, TMap> : IImportFileService<TResult>
        where TResult : class where TMap : ClassMap
    {
        private readonly ICSVReader _csvReader;

        public ImportCSVFileService(ICSVReader csvReader)
        {
            _csvReader = csvReader;
        }
        public async Task<Result<List<TResult>>> ImportAsync(
            string fileName,
            IBaseService<TResult> service)
        {
            try
            {
                var results = new List<TResult>();
                var path = GetPath(fileName);
                var records = _csvReader.Read<TResult, TMap>(path);
                
                var errors = new List<Error>();
                await HandleAdditionAsync(service, results, records, errors);
                return errors.Any()
                    ? Result<List<TResult>>.Failure(Errors.CSV.CSVNotValid(string.Join(',', errors.Select(e => e.ErrorMessage))))
                    : Result<List<TResult>>.Success(results);
            }
            catch (CsvHelperException)
            {
                return Result<List<TResult>>.Failure(Errors.CSV.CSVNotValid("CSV file is not valid"));
            }

            catch (Exception ex)
            {
                var errorMessage = $"An unexpected error occurred during the import process. Error: {ex.Message}";
                Console.WriteLine(errorMessage);
                return Result<List<TResult>>.Failure(Error.NotExpected);
            }
        }

        private static async Task HandleAdditionAsync(
            IBaseService<TResult> service,
            List<TResult> results,
            IEnumerable<TResult> records,
            List<Error> errors)
        {
            foreach (var record in records)
            {
                var result = await service.AddAsync(record);
                if (result.IsFailure)
                {
                    var message = $"Error while importing {record} to {service.GetType().Name}";
                    Console.WriteLine(message);
                    errors.Add(result.Error);
                }
                else
                {
                    results.Add(result.Value);
                }
            }
        }

        private static string GetPath(string filePath)
        {
            return Path.Combine(DocumentsFolderPath, "Storage", filePath);
        }
    }
}
