namespace RestaurantReservation.Contracts.Requests;
public record EmployeeRequest(
       int ResturantId,
       string FirstName,
       string LastName,
       string Position);

