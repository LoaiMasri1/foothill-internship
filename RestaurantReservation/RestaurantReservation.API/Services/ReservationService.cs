using AutoMapper;
using FluentValidation;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ReservationRequest> _validator;

    public ReservationService(
        IReservationRepository reservationRepository,
        IMapper mapper,
        IValidator<ReservationRequest> validator
    )
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ReservationResponse> CreateReservationAsync(
        ReservationRequest reservationRequest
    )
    {
        var validationResult = await _validator.ValidateAsync(reservationRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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
        var validationResult = await _validator.ValidateAsync(reservationRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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
