using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces
{
    public interface IResturantService
    {
        decimal CalculateRestaurantRevenue(int resturantId);
        Task<ResturantResponse> CreateResturantAsync(ResturantRequest resturantRequest);
        Task DeleteResturantAsync(int id);
        Task<ResturantResponse> UpdateResturantAsync(int id, ResturantRequest resturantRequest);
    }
}
