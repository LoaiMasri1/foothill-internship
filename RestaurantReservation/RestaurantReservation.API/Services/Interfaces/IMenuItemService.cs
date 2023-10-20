using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task<MenuItemResponse> CreateMenuItemAsync(MenuItemRequest menuItemRequest);
        Task DeleteMenuItemAsync(int id);
        Task<MenuItemResponse> UpdateMenuItemAsync(int id, MenuItemRequest menuItemRequest);
    }
}
