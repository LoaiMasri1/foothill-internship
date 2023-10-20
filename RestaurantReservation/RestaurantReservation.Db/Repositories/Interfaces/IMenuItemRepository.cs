using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<MenuItem> UpdateMenuItemAsync(int id, MenuItem menuItem);
    }
}
