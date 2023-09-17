namespace RestaurantReservation.Contracts.Responses;
public record OrderItemResponse(
    int OrderItemId,
    int OrderId,
    int ItemId,
    int Quantity);
