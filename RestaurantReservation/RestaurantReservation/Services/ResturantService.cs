using AutoMapper;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Services;

public class ResturantService
{
    private readonly ResturantRepository _resturantRepository;
    private readonly IMapper _mapper;

    public ResturantService(ResturantRepository resturantRepository, IMapper mapper)
    {
        _resturantRepository = resturantRepository;
        _mapper = mapper;
    }

    public async Task<ResturantResponse> CreateResturantAsync(ResturantRequest resturantRequest)
    {
        var newResturant = _mapper.Map<Resturant>(resturantRequest);
        await _resturantRepository.CreateResturantAsync(newResturant);
        var response = _mapper.Map<ResturantResponse>(newResturant);

        return response;
    }

    public async Task<ResturantResponse> UpdateResturantAsync(int id, ResturantRequest resturantRequest)
    {

        var updatedResturant = _mapper.Map<Resturant>(resturantRequest);

        var resturant = await _resturantRepository
            .UpdateResturantAsync(id, updatedResturant);

        var response = _mapper.Map<ResturantResponse>(resturant);

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
