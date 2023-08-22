namespace AirportTicket.Common.Services.Intf;

public interface IImportFileService<TResult> where TResult : class
{
    Task<Result<List<TResult>>> ImportAsync(string filePath, IBaseService<TResult> service);
}