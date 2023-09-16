namespace RestaurantReservation.Contracts.Responses;
public record ResturantResponse(
    int Id,
    string Name,
    string Address,
    int PhoneNumber,
    string OpeningHours);