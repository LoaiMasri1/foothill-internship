using System.ComponentModel.DataAnnotations;
using AirportTicket.Common;

namespace AirportTicket.Features.Flights.Models;

public class Flight : BsonEntityID
{
    public Guid FlightId { get; set; }
    public Departure Departure { get; set; } = null!;
    public Destination Destination { get; set; } = null!;
    public int AvailableSeats { get; set; }
    [Range(0.01, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
    public override string ToString() => $"From {Departure.Airport} to {Destination.Airport} on {Departure.Date} for {Price}";
}
