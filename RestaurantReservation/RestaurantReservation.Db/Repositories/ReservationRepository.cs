using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository
{
    private readonly RestaurantReservationDbContext _context;

    public ReservationRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation> CreateReservationAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();

        return reservation;
    }

    public async Task<Reservation> UpdateReservationAsync(int id, Reservation reservation)
    {
        var exist = await IsExistAsync(id);
        if (!exist)
        {
            throw new NotFoundException($"Reservation with id {id} does not exist");
        }
        _context.Reservations.Update(reservation);
        await _context.SaveChangesAsync();

        return reservation;
    }

    public async Task DeleteReservationAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id)
            ?? throw new NotFoundException($"Reservation with id {id} does not exist");

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var exists = await _context.Reservations.AnyAsync(x => x.ReservationsId == id);
        return exists;
    }
    public async Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(
        int customerId)
    {
        var reservations = await _context.Reservations
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();

        return reservations;
    }
}
