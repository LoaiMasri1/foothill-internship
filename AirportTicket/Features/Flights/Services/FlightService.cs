using AirportTicket.Common;
using AirportTicket.Common.Constants;
using AirportTicket.Common.Helper;
using AirportTicket.Common.Services;
using AirportTicket.Core;
using AirportTicket.Features.Flights.Models;

namespace AirportTicket.Features.Flights.Services;

public class FlightService : IFlightService
{
    private static readonly List<Flight> _flights;

    static FlightService()
    {
        _flights = GetFlights().Result;
    }
    private static async Task<List<Flight>> GetFlights()
    {
        var flights = await Storage.ReadAsync<Flight>();
        return flights.ToList();
    }


    public async Task<Result<Flight>> AddAsync(Flight entity)
    {

        var validationResult = ValidateFlight(entity);
        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        var isExist = _flights.Any(f => f.FlightId == entity.FlightId);
        if (isExist)
        {
            return Result<Flight>.Failure(Errors.Flight.FlightAlreadyExists);
        }

        _flights.Add(entity);
        await Storage.WriteAsync(_flights);
        return Result<Flight>.Success(entity);
    }

    public Result<Flight?> Get(Func<Flight, bool> predicate)
    {
        var flight = _flights.FirstOrDefault(predicate);
        if (flight is null)
        {
            return Result<Flight?>.Failure(Errors.Flight.FlightNotFound);
        }

        return Result<Flight?>.Success(flight);
    }

    public Result<ICollection<Flight>> GetAll()
    {
        return Result<ICollection<Flight>>.Success(_flights);
    }

    public Result<Flight> Update(Guid flightId, Flight entity)
    {
        var validationResult = ValidateFlight(entity);
        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        var flight = _flights.FirstOrDefault(f => f.FlightId == flightId);
        if (flight is null)
        {
            return Result<Flight>.Failure(Errors.Flight.FlightNotFound);
        }

        flight.Destination = entity.Destination ?? flight.Destination;
        flight.Departure = entity.Departure ?? flight.Departure;
        flight.AvailableSeats = entity.AvailableSeats == 0 ? flight.AvailableSeats : entity.AvailableSeats;
        Storage.WriteAsync(_flights).Wait();
        return Result<Flight>.Success(flight);
    }

    private static Result<Flight> ValidateFlight(Flight entity)
    {
        var validationResult = ValidationHelper.Validate(
            entity,
            Errors.Flight.FlightNotValid);
        if (validationResult.IsFailure)
        {
            return Result<Flight>.Failure(validationResult.Error);
        }

        var depratureResult = ValidationHelper.Validate(
            entity.Departure,
            Errors.Deprature.DepratureNotValid);

        if (depratureResult.IsFailure)
        {
            return Result<Flight>.Failure(depratureResult.Error);
        }

        var destinationResult = ValidationHelper.Validate(
            entity.Destination,
            Errors.Destination.DestinationNotValid);

        if (destinationResult.IsFailure)
        {
            return Result<Flight>.Failure(destinationResult.Error);
        }
        return Result<Flight>.Success(entity);
    }

    public static List<ValidationRule> GetValidationRules()
    {
        var flightRules = ValidationHelper.GetValidationRules<Flight>();
        var departureRules = ValidationHelper.GetValidationRules<Departure>();
        var destinationRules = ValidationHelper.GetValidationRules<Destination>();

        var rules = new List<ValidationRule>();
        rules.AddRange(flightRules);
        rules.AddRange(departureRules);
        rules.AddRange(destinationRules);

        return rules;
    }

    public Result<ICollection<Flight>> GetAll(Func<Flight, bool> predicate)
    {
        var flights = _flights.Where(predicate).ToList();
        if (flights.Count == 0)
        {
            return Result<ICollection<Flight>>.Failure(Errors.Flight.FlightNotFound);
        }

        return Result<ICollection<Flight>>.Success(flights);
    }
}
