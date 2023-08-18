using AirportTicket.Features.Flights.Models;

namespace AirportTicket.Common.Constants;

public static class FlightClasses
{
    public static readonly FlightClassInfo Economy = new(Guid.NewGuid(), "Economy", 100);
    public static readonly FlightClassInfo Business = new(Guid.NewGuid(), "Business", 200);
    public static readonly FlightClassInfo First = new(Guid.NewGuid(), "First", 300);
}
