namespace RestaurantReservation.Contracts.Requests;
public record OrderRequest(
      int EmployeeId,
      int ReservationId,
      DateTime OrderDate,
      int TotalAmount);
