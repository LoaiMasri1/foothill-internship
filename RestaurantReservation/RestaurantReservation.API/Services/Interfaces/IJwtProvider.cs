using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Services.Interfaces;
public interface IJwtProvider
{
    string Generate(Customer customer);
}
