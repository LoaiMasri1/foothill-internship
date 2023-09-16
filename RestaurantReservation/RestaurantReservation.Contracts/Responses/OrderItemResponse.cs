namespace RestaurantReservation.Contracts.Responses;
public record OrderItemResponse(
    int Id,
    int OrderId,
    int ItemId,
    int Quantity);
