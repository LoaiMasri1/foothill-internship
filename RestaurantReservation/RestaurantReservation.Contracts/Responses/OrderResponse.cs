namespace RestaurantReservation.Contracts.Responses;
public record OrderResponse(
    int Id,
    int EmployeeId,
    int ReservationId,
    DateTime OrderDate,
    int TotalAmount);
