namespace RestaurantReservation.Contracts.Responses;

public record CustomerResponse(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    int PhoneNumber);