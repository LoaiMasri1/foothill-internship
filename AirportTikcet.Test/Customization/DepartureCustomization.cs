using AirportTicket.Features.Flights.Models;

namespace AirportTikcet.Test.Customization;

public class DepartureCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var departureDate = DateTime.Now.AddDays(1);
        var flight = fixture.Build<Departure>()
                            .With(d => d.Date, departureDate)
                            .Create();

        fixture.Inject(flight);
    }
}