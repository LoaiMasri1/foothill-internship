using AutoMapper;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Services;

public class MenuItemService
{
    private readonly MenuItemRepository _menuItemRepository;
    private readonly IMapper _mapper;

    public MenuItemService(MenuItemRepository menuItemRepository,IMapper mapper)
    {
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }

    public async Task<MenuItemResponse> CreateMenuItemAsync(MenuItemRequest menuItemRequest)
    {
        var newMenuItem = _mapper.Map<MenuItem>(menuItemRequest);
        await _menuItemRepository.CreateMenuItemAsync(newMenuItem);

        var response = _mapper.Map<MenuItemResponse>(newMenuItem);
                       
        return response;
    }

    public async Task<MenuItemResponse> UpdateMenuItemAsync(
        int id,
        MenuItemRequest menuItemRequest)
    {
        var updatedMenuItem = _mapper.Map<MenuItem>(menuItemRequest);

        var menuItem = await _menuItemRepository
            .UpdateMenuItemAsync(id, updatedMenuItem);

        var response = _mapper.Map<MenuItemResponse>(menuItem);

        return response;
    }


    public async Task DeleteMenuItemAsync(int id)
    {
        await _menuItemRepository.DeleteMenuItemAsync(id);
    }
}
