namespace RestaurantReservation.Contracts.Responses;

public record OrderMenuItemResponse(
    int OrderId,
    IEnumerable<MenuItemResponse> MenuItems,
    int Quantity
);
