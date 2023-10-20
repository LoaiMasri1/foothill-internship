using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IResturantRepository
    {
        decimal CalculateRestaurantRevenueAsync(int resturantId);
        Task<Resturant> CreateResturantAsync(Resturant resturant);
        Task DeleteResturantAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<Resturant> UpdateResturantAsync(int id, Resturant resturant);
    }
}
