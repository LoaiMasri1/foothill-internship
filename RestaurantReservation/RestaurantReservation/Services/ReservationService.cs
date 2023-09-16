using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class ReservationService
{
    private readonly RestaurantReservationDbContext _context;

    public ReservationService(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<ReservationResponse> CreateReservationAsync(ReservationRequest reservationRequest)
    {

        var isCustomerExist = await _context.Customers
            .AnyAsync(x => x.CustomerId == reservationRequest.CustomerId);

        var isRestaurantExist = await _context.Resturants
            .AnyAsync(x => x.ResturantsId == reservationRequest.RestaurantId);

        var isTableExist = await _context.Tables
            .AnyAsync(x => x.TableId == reservationRequest.TableId);

        if (!isCustomerExist)
        {
            throw new NotFoundException($"Customer with id {reservationRequest.CustomerId} does not exist");
        }

        if (!isRestaurantExist)
        {
            throw new NotFoundException($"Restaurant with id {reservationRequest.RestaurantId} does not exist");
        }

        if (!isTableExist)
        {
            throw new NotFoundException($"Table with id {reservationRequest.TableId} does not exist");
        }

        var newReservation = new Reservation
        {
            CustomerId = reservationRequest.CustomerId,
            ResturantId = reservationRequest.RestaurantId,
            TableId = reservationRequest.TableId,
            ReservationDate = reservationRequest.ReservationDate,
            PartySize = reservationRequest.PartySize
        };

        await _context.Reservations.AddAsync(newReservation);
        await _context.SaveChangesAsync();

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
        var reservation = await _context.Reservations.FindAsync(id)
            ?? throw new NotFoundException($"Reservation with id {id} does not exist");

        var isCustomerExist = await _context.Customers
            .AnyAsync(x => x.CustomerId == reservationRequest.CustomerId);

        var isRestaurantExist = await _context.Resturants
            .AnyAsync(x => x.ResturantsId == reservationRequest.RestaurantId);

        var isTableExist = await _context.Tables
            .AnyAsync(x => x.TableId == reservationRequest.TableId);

        if (!isCustomerExist)
        {
            throw new NotFoundException($"Customer with id {reservationRequest.CustomerId} does not exist");
        }

        if (!isRestaurantExist)
        {
            throw new NotFoundException($"Restaurant with id {reservationRequest.RestaurantId} does not exist");
        }

        if (!isTableExist)
        {
            throw new NotFoundException($"Table with id {reservationRequest.TableId} does not exist");
        }

        reservation.CustomerId = reservationRequest.CustomerId;
        reservation.ResturantId = reservationRequest.RestaurantId;
        reservation.TableId = reservationRequest.TableId;
        reservation.ReservationDate = reservationRequest.ReservationDate;
        reservation.PartySize = reservationRequest.PartySize;

        await _context.SaveChangesAsync();

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
        var reservation = await _context.Reservations.FindAsync(id)
            ?? throw new NotFoundException($"Reservation with id {id} does not exist");

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<ReservationResponse>> GetReservationsByCustomerAsync(
        int customerId
        )
    {
        var reservations = await _context.Reservations
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();

        var response = reservations.Select(x => new ReservationResponse(
            x.ReservationsId,
            x.CustomerId,
            x.ResturantId,
            x.TableId,
            x.ReservationDate,
            x.PartySize)).ToList();

        return response;
        

    }

}
