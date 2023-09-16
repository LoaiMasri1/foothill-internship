namespace RestaurantReservation.Contracts.Requests;
public record ResturantRequest(
      string Name,
      string Address,
      int PhoneNumber,
      string OpeningHours);
