using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Repositories;

namespace RestaurantReservation.Services;

public class ReservationService
{
    private readonly ReservationRepository _reservationRepository;

    public ReservationService(ReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<ReservationResponse> CreateReservationAsync(ReservationRequest reservationRequest)
    {

        var newReservation = new Reservation
        {
            CustomerId = reservationRequest.CustomerId,
            ResturantId = reservationRequest.RestaurantId,
            TableId = reservationRequest.TableId,
            ReservationDate = reservationRequest.ReservationDate,
            PartySize = reservationRequest.PartySize
        };

        await _reservationRepository.CreateReservationAsync(newReservation);

        var response = new ReservationResponse(
            newReservation.ReservationsId,
            newReservation.CustomerId,
            newReservation.ResturantId,
            newReservation.TableId,
            newReservation.ReservationDate,
            newReservation.PartySize);

        return response;
    }


    public async Task<ReservationResponse> UpdateReservationAsync(int id, ReservationRequest reservationRequest)
    {
        var updatedRservation = new Reservation
        {
            CustomerId = reservationRequest.CustomerId,
            ResturantId = reservationRequest.RestaurantId,
            TableId = reservationRequest.TableId,
            ReservationDate = reservationRequest.ReservationDate,
            PartySize = reservationRequest.PartySize
        };

        var reservation = await _reservationRepository
            .UpdateReservationAsync(id, updatedRservation);

        var response = new ReservationResponse(
            reservation.ReservationsId,
            reservation.CustomerId,
            reservation.ResturantId,
            reservation.TableId,
            reservation.ReservationDate,
            reservation.PartySize);

        return response;
    }

    public async Task DeleteReservationAsync(int id)
    {
        await _reservationRepository.DeleteReservationAsync(id);
    }

    public async Task<IEnumerable<ReservationResponse>> GetReservationsByCustomerAsync(
        int customerId
        )
    {
        var reservations = await _reservationRepository.GetReservationsByCustomerAsync(customerId); 
        var response = reservations.Select(x => new ReservationResponse(
            x.ReservationsId,
            x.CustomerId,
            x.ResturantId,
            x.TableId,
            x.ReservationDate,
            x.PartySize));
       
        return response;
    }

}
