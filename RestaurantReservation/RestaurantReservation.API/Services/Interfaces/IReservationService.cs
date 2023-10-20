using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResponse> CreateReservationAsync(ReservationRequest reservationRequest);
        Task DeleteReservationAsync(int id);
        Task<IEnumerable<ReservationResponse>> GetReservationsByCustomerAsync(int customerId);
        Task<ReservationResponse> UpdateReservationAsync(
            int id,
            ReservationRequest reservationRequest
        );
    }
}
