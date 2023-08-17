using AirportTicket.Common;
using AirportTicket.Common.Services.Intf;
using AirportTicket.Features.Bookings.Models;

namespace AirportTicket.Features.Bookings.Services;

public interface IBookingService : IBaseService<Booking>
{
    Task<Result<Booking>> DeleteAsync(Guid id);
}
