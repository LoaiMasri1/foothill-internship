using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Services;

namespace RestaurantReservation.Repositories;

public class MenuItemRepository
{
    private readonly RestaurantReservationDbContext _context;

    public MenuItemRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem)
    {
        await _context.MenuItems.AddAsync(menuItem);
        await _context.SaveChangesAsync();

        return menuItem;
    }

    public async Task<MenuItem> UpdateMenuItemAsync(int id, MenuItem menuItem)
    {
        var exist = await IsExistAsync(id);
        if (!exist)
        {
            throw new NotFoundException($"MenuItem with id {id} does not exist");
        }
        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();

        return menuItem;
    }

    public async Task DeleteMenuItemAsync(int id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id)
            ?? throw new NotFoundException($"MenuItem with id {id} does not exist");

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var exists = await _context.MenuItems.AnyAsync(x => x.MenuItemId == id);
        return exists;
    }

}
