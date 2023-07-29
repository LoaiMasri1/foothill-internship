using AirportTicket.Common.Constants;
using AirportTicket.Common.Helper;
using AirportTicket.Common.Models;
using AirportTicket.Common;
using AirportTicket.Core;
using AirportTicket.Features.Bookings.Models;
using AirportTicket.Features.Flights.Services;
using AirportTicket.Features.Flights.Models;

namespace AirportTicket.Features.Bookings.Services;

public class BookingService:IBookingService
{
    private static readonly List<Booking> _bookings;
    private readonly FlightService _flightService;

    static BookingService()
    {
        _bookings = GetBookings().Result;
    }

    public BookingService()
    {
        _flightService = new FlightService();
    }

    private static async Task<List<Booking>> GetBookings()
    {
        var bookings = await Storage.ReadAsync<Booking>();
        return bookings.ToList();
    }
    public async Task<Result<Booking>> AddAsync(Booking entity)
    {

        var validationResult = ValidationHelper.Validate(
            entity,
            Errors.Booking.BookingNotValid);
        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        var isExist = _bookings.Any(IsMatchingBooking(entity));
        if (isExist)
        {
            return Result<Booking>.Failure(Errors.Booking.BookingAlreadyExists);
        }

        var flightResult = _flightService.Get(f => f.FlightId == entity.Flight.FlightId);

        if (flightResult.IsFailure)
        {
            return Result<Booking>.Failure(flightResult.Error);
        }

        var flight = flightResult.Value;

        if (flight!.AvailableSeats == 0)
        {
            return Result<Booking>.Failure(Errors.Flight.NoAvailableSeats);
        }

        entity.TotalPrice = flight!.Price + entity.ClassInfo.Price;

        DecreaseFlightAvailableSeatsIfExist(entity.Flight.FlightId);


        _bookings.Add(entity);

        await Storage.WriteAsync(_bookings);
        return Result<Booking>.Success(entity);
    }

    public async Task<Result<Booking>> DeleteAsync(Guid id)
    {
        var booking = _bookings.FirstOrDefault(b => b.Id == id);
        if (booking is null)
        {
            return Result<Booking>.Failure(Errors.Booking.BookingNotFound);
        }

        IncreaseFlightAvailableSeats(booking.Flight.FlightId);

        _bookings.Remove(booking);

        await Storage.WriteAsync(_bookings);

        return Result<Booking>.Success(booking);
    }

    public Result<Booking?> Get(Func<Booking, bool> predicate)
    {
        var booking = _bookings.FirstOrDefault(predicate);
        return booking is null
            ? Result<Booking?>.Failure(Errors.Booking.BookingNotFound)
            : Result<Booking?>.Success(booking);
    }

    public Result<ICollection<Booking>> GetAll()
    {
        return Result<ICollection<Booking>>.Success(_bookings);
    }

    public Result<ICollection<Booking>> GetAll(Func<Booking, bool> predicate)
    {
        var bookings = _bookings.Where(predicate).ToList();
        return bookings.Any()
            ? Result<ICollection<Booking>>.Success(bookings)
            : Result<ICollection<Booking>>.Failure(Errors.Booking.BookingNotFound);
    }

    public Result<Booking> Update(Guid id, Booking entity)
    {
        var validationResult = ValidationHelper.Validate(
            entity,
            Errors.Booking.BookingNotValid);
        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        var booking = _bookings.FirstOrDefault(b => b.Id == id);
        if (booking is null)
        {
            return Result<Booking>.Failure(Errors.Booking.BookingNotFound);
        }

        var flightResult = _flightService.Get(f => f.FlightId == entity.Flight.FlightId);

        if (flightResult.IsFailure)
        {
            return Result<Booking>.Failure(flightResult.Error);
        }

        var flight = flightResult.Value;

        booking.Flight = entity.Flight;
        booking.Passenger = entity.Passenger;
        booking.DateUtc = entity.DateUtc;
        booking.ClassInfo = entity.ClassInfo;
        booking.TotalPrice = flight!.Price + entity.ClassInfo.Price;


        Storage.WriteAsync(_bookings).Wait();

        return Result<Booking>.Success(entity);
    }

    private void DecreaseFlightAvailableSeatsIfExist(Guid flightId)
    {
        var flightResult = _flightService.Get(f => f.FlightId == flightId);

        if (flightResult.IsFailure)
        {
            Console.WriteLine(flightResult.Error);
            return;
        }

        var flight = flightResult.Value;

        if (flight!.AvailableSeats == 0)
        {
            Console.WriteLine(Errors.Flight.NoAvailableSeats);
            return;
        }

        _flightService.Update(flightId, new Flight
        {
            AvailableSeats = flight!.AvailableSeats - 1,
        });
    }

    private void IncreaseFlightAvailableSeats(Guid flightId)
    {
        var flightResult = _flightService.Get(f => f.FlightId == flightId);

        if (flightResult.IsFailure)
        {
            return;
        }

        var flight = flightResult.Value;

        _flightService.Update(flightId, new Flight
        {
            AvailableSeats = flight!.AvailableSeats + 1,
        });
    }
    private static Func<Booking, bool> IsMatchingBooking(Booking first)
    {
        return second => second.Flight.FlightId == first.Flight.FlightId
                        && second.Passenger.Id == first.Passenger.Id
                        && second.DateUtc == first.DateUtc;
    }

    public static List<ValidationRule> GetValidationRules()
    {
        return ValidationHelper.GetValidationRules<Booking>();
    }
}
