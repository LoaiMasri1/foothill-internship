namespace RestaurantReservation.Contracts.Responses;
public record MenuItemResponse(
      int ItemId,
      int RestaurantId,
      string Name,
      string Description,
      decimal Price);
