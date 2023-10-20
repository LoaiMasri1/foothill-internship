using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface ITableRepository
    {
        Task<Table> CreateTableAsync(Table table);
        Task DeleteTableAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<Table> UpdateTableAsync(int id, Table table);
    }
}
