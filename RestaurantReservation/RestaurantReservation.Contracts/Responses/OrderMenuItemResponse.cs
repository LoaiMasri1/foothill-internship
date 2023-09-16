using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.Services;

public record OrderMenuItemResponse(
    int OrderId,
    IEnumerable<MenuItemResponse> MenuItems,
    int Quantity);
