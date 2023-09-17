using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Repositories;

namespace RestaurantReservation.Services;

public class ResturantService
{
    private readonly ResturantRepository _resturantRepository;

    public ResturantService(ResturantRepository resturantRepository)
    {
        _resturantRepository = resturantRepository;
    }

    public async Task<ResturantResponse> CreateResturantAsync(ResturantRequest resturantRequest)
    {
        var newResturant = new Resturant
        {
            Name = resturantRequest.Name,
            Address = resturantRequest.Address,
            PhoneNumber = resturantRequest.PhoneNumber,
            OpeningHours = resturantRequest.OpeningHours
        };
        await _resturantRepository.CreateResturantAsync(newResturant);
        var response = new ResturantResponse(
        newResturant.ResturantsId,
        newResturant.Name,
        newResturant.Address,
        newResturant.PhoneNumber,
        newResturant.OpeningHours);

        return response;
    }

    public async Task<ResturantResponse> UpdateResturantAsync(int id, ResturantRequest resturantRequest)
    {

        var updatedResturant = new Resturant
        {
            Name = resturantRequest.Name,
            Address = resturantRequest.Address,
            PhoneNumber = resturantRequest.PhoneNumber,
            OpeningHours = resturantRequest.OpeningHours
        };

        var resturant = await _resturantRepository
            .UpdateResturantAsync(id, updatedResturant);

        var response = new ResturantResponse(
     resturant.ResturantsId,
     resturant.Name,
     resturant.Address,
     resturant.PhoneNumber,
     resturant.OpeningHours);

        return response;
    }

    public async Task DeleteResturantAsync(int id)
    {
        await _resturantRepository.DeleteResturantAsync(id);
    }

    public decimal CalculateRestaurantRevenueAsync(int resturantId)
    {
        var revenue = _resturantRepository.CalculateRestaurantRevenueAsync(resturantId);
        return revenue;

    }

}
