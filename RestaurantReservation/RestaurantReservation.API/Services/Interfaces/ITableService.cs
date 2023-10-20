using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces
{
    public interface ITableService
    {
        Task<TableResponse> CreateTableAsync(TableRequest tableRequest);
        Task DeleteTableAsync(int id);
        Task<TableResponse> UpdateTableAsync(int id, TableRequest tableRequest);
    }
}
