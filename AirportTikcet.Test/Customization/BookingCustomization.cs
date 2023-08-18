using AirportTicket.Features.Bookings.Models;

namespace AirportTikcet.Test.Customization;

class BookingCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var futureDate = DateTime.UtcNow.AddDays(1);
        fixture.Customize(new DepartureCustomization());
        var booking = fixture.Build<Booking>()
                            .With(b => b.DateUtc, futureDate)
                            .Create();

        fixture.Inject(booking);
    }
}
