using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Repositories;

namespace RestaurantReservation.Services;

public class MenuItemService
{
    private readonly MenuItemRepository _menuItemRepository;

    public MenuItemService(MenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
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

        await _menuItemRepository.CreateMenuItemAsync(newMenuItem);

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
        var updatedMenuItem = new MenuItem
        {
            Name = menuItemRequest.Name,
            Description = menuItemRequest.Description,
            Price = menuItemRequest.Price,
            ResturantId = menuItemRequest.RestaurantId
        };

        var menuItem = await _menuItemRepository
            .UpdateMenuItemAsync(id, updatedMenuItem);

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
        await _menuItemRepository.DeleteMenuItemAsync(id);
    }
}
