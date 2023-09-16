namespace RestaurantReservation.Contracts.Requests;
public record ReservationRequest(
      int CustomerId,
      int RestaurantId,
      int TableId,
      DateTime ReservationDate,
      int PartySize);
