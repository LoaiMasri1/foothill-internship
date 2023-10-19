namespace RestaurantReservation.Contracts.Responses;

public record CustomerResponse(
    int CustomerId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int PhoneNumber);