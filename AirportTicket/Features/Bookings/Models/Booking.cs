using AirportTicket.Common.CustomAttribute;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Users.Models;
using System.ComponentModel.DataAnnotations;

namespace AirportTicket.Features.Bookings.Models;

public class Booking
{
    public Guid Id { get; set; }
    public User Passenger { get; set; } = null!;
    public Flight Flight { get; set; } = null!;
    [Required(ErrorMessage = "DateUtc is required.")]
    [FutureDate(ErrorMessage = "Date should be in the future.")]
    public DateTime DateUtc { get; set; }
    [Required(ErrorMessage = "ClassInfo is required.")]
    public FlightClassInfo ClassInfo { get; set; } = null!;
    [Range(0.01, int.MaxValue, ErrorMessage = "TotalPrice must be greater than 0.")]
    public decimal TotalPrice { get; set; }


    public static Booking Create(
        User passenger,
        Flight flight,
        FlightClassInfo classInfo,
        DateTime dateUtc)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            Passenger = passenger,
            Flight = flight,
            DateUtc = dateUtc,
            TotalPrice = flight!.Price + classInfo.Price,
            ClassInfo = classInfo,
        };

        return booking;
    }

    public override string ToString() =>
        $"Booking {Id} for {Passenger.Id} on {Flight.FlightId} for {TotalPrice}";
}