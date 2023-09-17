namespace RestaurantReservation.Contracts.Responses;
public record OrderResponse(
    int OrderId,
    int EmployeeId,
    int ReservationId,
    DateTime OrderDate,
    int TotalAmount);
