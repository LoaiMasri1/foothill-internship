using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class MenuItemService
{
    private readonly RestaurantReservationDbContext _context;

    public MenuItemService(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<MenuItemResponse> CreateMenuItemAsync(MenuItemRequest menuItemRequest)
    {
        var newMenuItem = new MenuItem
        {
            Name = menuItemRequest.Name,
            Description = menuItemRequest.Description,
            Price = menuItemRequest.Price,
            ResturantId = menuItemRequest.RestaurantId
        };

        await _context.MenuItems.AddAsync(newMenuItem);
        await _context.SaveChangesAsync();

        var response = new MenuItemResponse(
            newMenuItem.ItemId,
            newMenuItem.ResturantId,
            newMenuItem.Name,
            newMenuItem.Description,
            newMenuItem.Price);
                       
        return response;
    }

    public async Task<MenuItemResponse> UpdateMenuItemAsync(int id, MenuItemRequest menuItemRequest)
    {
        var menuItem = await _context.MenuItems.FindAsync(id)
            ?? throw new NotFoundException($"Menu item with id {id} does not exist");

        var isRestaurantExist = await _context.Resturants
            .AnyAsync(x => x.ResturantsId == menuItemRequest.RestaurantId);

        if (!isRestaurantExist)
        {
            throw new NotFoundException($"Restaurant with id {menuItemRequest.RestaurantId} does not exist");
        }

        menuItem.Name = menuItemRequest.Name;
        menuItem.Description = menuItemRequest.Description;
        menuItem.Price = menuItemRequest.Price;
        menuItem.ResturantId = menuItemRequest.RestaurantId;

        await _context.SaveChangesAsync();

        var response = new MenuItemResponse(
            menuItem.ItemId,
            menuItem.ResturantId,
            menuItem.Name,
            menuItem.Description,
            menuItem.Price);

        return response;
    }


    public async Task DeleteMenuItemAsync(int id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id)
            ?? throw new NotFoundException($"Menu item with id {id} does not exist");

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
    }
}
