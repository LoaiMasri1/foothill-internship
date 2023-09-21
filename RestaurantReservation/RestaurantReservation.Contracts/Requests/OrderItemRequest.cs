namespace RestaurantReservation.Contracts.Requests;
public record OrderItemRequest(
    int OrderId,
    int ItemId,
    int Quantity);
