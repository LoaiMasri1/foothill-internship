using AirportTicket.Features.Flights.Models;
using CsvHelper.Configuration;

namespace AirportTicket.Core.Configuration;

public sealed class FlightMap : ClassMap<Flight>
{
    public FlightMap()
    {
        Map(m => m.FlightId).Name("FlightId");
        Map(m => m.Departure.Id).Name("Departure.Id");
        Map(m => m.Departure.Country).Name("Departure.Country");
        Map(m => m.Departure.Date).Name("Departure.Date");
        Map(m => m.Departure.Airport).Name("Departure.Airport");
        Map(m => m.Destination.Country).Name("Destination.Country");
        Map(m => m.Destination.Airport).Name("Destination.Airport");
        Map(m => m.AvailableSeats).Name("AvailableSeats");
        Map(m => m.Price).Name("Price");
    }
}
