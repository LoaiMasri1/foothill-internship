namespace RestaurantReservation.Contracts.Responses;
public record TableResponse(
    int TableId,
    int ResturantId,
    int Capacity);
