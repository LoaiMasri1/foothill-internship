namespace RestaurantReservation.Contracts.Responses;
public record ResturantResponse(
    int ResturantsId,
    string Name,
    string Address,
    int PhoneNumber,
    string OpeningHours);