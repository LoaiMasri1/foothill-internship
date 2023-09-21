namespace RestaurantReservation.Contracts.Requests;
public record TableRequest(
      int RestaurantId,
      int Capacity);
