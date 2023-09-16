namespace RestaurantReservation.Contracts.Requests;
public record MenuItemRequest(
    
      int RestaurantId,
      string Name,
      string Description,
      decimal Price);
