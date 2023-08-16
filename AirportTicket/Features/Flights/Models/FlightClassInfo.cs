namespace AirportTicket.Features.Flights.Models;

public class FlightClassInfo
{
    public Guid Id { get; private set; }
    public string ClassName { get; private set; }
    public decimal Price { get; private set; }
    public FlightClassInfo(Guid id, string className, decimal price)
    {
        Id = id;
        ClassName = className;
        Price = price;
    }
    public override string ToString() => $"Class {ClassName} with price {Price}";
}
