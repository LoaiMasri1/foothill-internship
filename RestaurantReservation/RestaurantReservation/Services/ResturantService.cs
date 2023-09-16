using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class ResturantService
{
    private readonly RestaurantReservationDbContext _context;

    public ResturantService(RestaurantReservationDbContext context)
    {
        _context = context;
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

        await _context.Resturants.AddAsync(newResturant);
        await _context.SaveChangesAsync();

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
        var resturant = await _context.Resturants.FindAsync(id)
            ?? throw new NotFoundException($"Resturant with id {id} does not exist");

        resturant.Name = resturantRequest.Name;
        resturant.Address = resturantRequest.Address;
        resturant.PhoneNumber = resturantRequest.PhoneNumber;
        resturant.OpeningHours = resturantRequest.OpeningHours;

        await _context.SaveChangesAsync();

        var response = new ResturantResponse(
            resturant.ResturantsId,
            resturant.Name,
            resturant.Address,
            resturant.PhoneNumber,
            resturant.OpeningHours);

        return response;
    }

    public async Task<ResturantResponse> GetResturantAsync(int id)
    {
        var resturant = await _context.Resturants.FindAsync(id)
            ?? throw new NotFoundException($"Resturant with id {id} does not exist");

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
        var resturant = await _context.Resturants.FindAsync(id)
            ?? throw new NotFoundException($"Resturant with id {id} does not exist");

        _context.Resturants.Remove(resturant);
        await _context.SaveChangesAsync();
    }

    public decimal CalculateRestaurantRevenueAsync(int resturantId)
    {
        var revenue = _context.Resturants
            .Select(r => _context.CalculateRestaurantRevenue(resturantId))
            .FirstOrDefault();

        return revenue;
    }

}
