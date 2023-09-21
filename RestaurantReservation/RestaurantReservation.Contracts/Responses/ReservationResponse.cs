namespace RestaurantReservation.Contracts.Responses;
public record ReservationResponse(
    int ReservationsId,
    int CustomerId,
    int ResturantId,
    int TableId,
    DateTime ReservationDate,
    int PartySize);
