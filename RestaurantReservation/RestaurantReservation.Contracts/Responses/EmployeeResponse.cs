namespace RestaurantReservation.Contracts.Responses;
public record EmployeeResponse(
       int EmployeeId,
       int ResturantId,
       string FirstName,
       string LastName,
       string Position
);

