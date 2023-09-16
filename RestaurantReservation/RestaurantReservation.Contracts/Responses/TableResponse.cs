namespace RestaurantReservation.Contracts.Responses;
public record TableResponse(
    int Id,
    int RestaurantId,
    int Capacity);
