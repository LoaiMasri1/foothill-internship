using AutoMapper;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Services;

public class ReservationService
{
    private readonly ReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ReservationService(ReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<ReservationResponse> CreateReservationAsync(
        ReservationRequest reservationRequest
    )
    {
        var newReservation = _mapper.Map<Reservation>(reservationRequest);

        await _reservationRepository.CreateReservationAsync(newReservation);

        var response = _mapper.Map<ReservationResponse>(newReservation);

        return response;
    }

    public async Task<ReservationResponse> UpdateReservationAsync(
        int id,
        ReservationRequest reservationRequest
    )
    {
        var updatedRservation = _mapper.Map<Reservation>(reservationRequest);

        var reservation = await _reservationRepository.UpdateReservationAsync(
            id,
            updatedRservation
        );

        var response = _mapper.Map<ReservationResponse>(reservation);

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
        var response = reservations.Select(x => _mapper.Map<ReservationResponse>(x));

        return response;
    }
}
