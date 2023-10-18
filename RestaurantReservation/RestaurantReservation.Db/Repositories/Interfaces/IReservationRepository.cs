using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId);
        Task<bool> IsExistAsync(int id);
        Task<Reservation> UpdateReservationAsync(int id, Reservation reservation);
    }
}
