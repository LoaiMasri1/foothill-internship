using System.Globalization;
using static AirportTicket.Common.Constants.PathConstants;
using AirportTicket.Common.Services.Intf;
using CsvHelper;
using CsvHelper.Configuration;
using AirportTicket.Common.Constants;

namespace AirportTicket.Common.Services.Impl
{
    public class ImportCSVFileService<TResult, TMap> : IImportFileService<TResult>
        where TResult : class where TMap : ClassMap
    {
        public async Task<Result<List<TResult>>> ImportAsync(
            string fileName,
            IBaseService<TResult> service)
        {
            try
            {
                var results = new List<TResult>();
                var path = GetPath(fileName);
                MoveSampleDataToDocument();

                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<TMap>();
                var records = csv.GetRecords<TResult>();
                var errors = new List<Error>();
                await HandleAdditionAsync(service, results, records, errors);

                return errors.Any()
                    ? Result<List<TResult>>.Failure(Errors.CSV.CSVNotValid(string.Join(',', errors.Select(e => e.ErrorMessage))))
                    : Result<List<TResult>>.Success(results);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An unexpected error occurred during the import process. Error: {ex.Message}";
                Console.WriteLine(errorMessage);
                return Result<List<TResult>>.Failure(Error.NotExpected);
            }
        }

        private static void MoveSampleDataToDocument()
        {
            var destinationPath = Path.Combine(DocumentsFolderPath, "Storage");
            Directory.CreateDirectory(destinationPath);
            var destinationFile = Path.Combine(destinationPath, "SampleData.csv");
            File.Copy(SampleDataPath, destinationFile, true);
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
