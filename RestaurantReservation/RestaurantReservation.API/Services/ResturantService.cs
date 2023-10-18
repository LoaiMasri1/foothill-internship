using AutoMapper;
using FluentValidation;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Services;

public class ResturantService : IResturantService
{
    private readonly IResturantRepository _resturantRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ResturantRequest> _validator;

    public ResturantService(
        IResturantRepository resturantRepository,
        IMapper mapper,
        IValidator<ResturantRequest> validator
    )
    {
        _resturantRepository = resturantRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ResturantResponse> CreateResturantAsync(ResturantRequest resturantRequest)
    {
        var validationResult = await _validator.ValidateAsync(resturantRequest);
        if (validationResult != null)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var newResturant = _mapper.Map<Resturant>(resturantRequest);
        await _resturantRepository.CreateResturantAsync(newResturant);
        var response = _mapper.Map<ResturantResponse>(newResturant);

        return response;
    }

    public async Task<ResturantResponse> UpdateResturantAsync(
        int id,
        ResturantRequest resturantRequest
    )
    {
        var validationResult = await _validator.ValidateAsync(resturantRequest);
        if (validationResult != null)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var updatedResturant = _mapper.Map<Resturant>(resturantRequest);

        var resturant = await _resturantRepository.UpdateResturantAsync(id, updatedResturant);

        var response = _mapper.Map<ResturantResponse>(resturant);

        return response;
    }

    public async Task DeleteResturantAsync(int id)
    {
        await _resturantRepository.DeleteResturantAsync(id);
    }

    public decimal CalculateRestaurantRevenue(int resturantId)
    {
        var revenue = _resturantRepository.CalculateRestaurantRevenueAsync(resturantId);
        return revenue;
    }
}
