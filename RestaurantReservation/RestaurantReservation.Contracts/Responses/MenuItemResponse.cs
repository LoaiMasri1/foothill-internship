namespace RestaurantReservation.Contracts.Responses;
public record MenuItemResponse(
      int Id,
      int RestaurantId,
      string Name,
      string Description,
      decimal Price);
