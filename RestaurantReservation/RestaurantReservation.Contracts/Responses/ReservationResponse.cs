namespace RestaurantReservation.Contracts.Responses;
public record ReservationResponse(
    int Id,
    int CustomerId,
    int RestaurantId,
    int TableId,
    DateTime ReservationDate,
    int PartySize);
