namespace RestaurantReservation.Contracts.Responses;
public record EmployeeResponse(
       int Id,
       int ResturantId,
       string FirstName,
       string LastName,
       string Position
);

