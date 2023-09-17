using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Services;

namespace RestaurantReservation.Repositories;

public class ResturantRepository
{
    private readonly RestaurantReservationDbContext _context;

    public ResturantRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Resturant> CreateResturantAsync(Resturant resturant)
    {
        await _context.Resturants.AddAsync(resturant);
        await _context.SaveChangesAsync();

        return resturant;
    }

    public async Task<Resturant> UpdateResturantAsync(int id, Resturant resturant)
    {
        var exist = await IsExistAsync(id);
        if (!exist)
        {
            throw new NotFoundException($"Resturant with id {id} does not exist");
        }
        _context.Resturants.Update(resturant);
        await _context.SaveChangesAsync();

        return resturant;
    }

    public async Task DeleteResturantAsync(int id)
    {
        var resturant = await _context.Resturants.FindAsync(id)
            ?? throw new NotFoundException($"Resturant with id {id} does not exist");

        _context.Resturants.Remove(resturant);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var exists = await _context.Resturants.AnyAsync(x => x.ResturantsId == id);
        return exists;
    }
    public decimal CalculateRestaurantRevenueAsync(int resturantId)
    {
        var revenue = _context.Resturants
            .Select(r => _context.CalculateRestaurantRevenue(resturantId))
            .FirstOrDefault();

        return revenue;
    }
}
