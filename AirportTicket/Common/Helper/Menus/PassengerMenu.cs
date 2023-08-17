using AirportTicket.Common.Constants;
using AirportTicket.Core;
using AirportTicket.Features.Bookings.Models;
using AirportTicket.Features.Bookings.Services;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Flights.Services;
using AirportTicket.Features.Users.Models;

namespace AirportTicket.Common.Helper.Menus;

public class PassengerMenu
{
    private static readonly IStorage _storage = Storage.GetInstance();
    private static readonly FlightService _flightService = new(_storage);
    private static readonly IBookingService _bookingService = new BookingService(_flightService, _storage);

    private static void Show()
    {
        Console.WriteLine("1. Book a flight");
        Console.WriteLine("2. Cancel a flight");
        Console.WriteLine("3. View my bookings");
        Console.WriteLine("4. Exit");
    }




    public static async Task Handle(User user)
    {
        while (true)
        {
            Show();
            var passengerInput = Console.ReadLine();

            switch (passengerInput)
            {
                case "1":
                    await BookFlight(user);
                    break;
                case "2":
                    await CancelBooking(user);
                    break;
                case "3":
                    ViewBookings(user);
                    break;
                case "4":
                    AppMenu.Exit();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private static async Task BookFlight(User user)
    {
        Console.WriteLine("Book a flight");

        var flights = ListFlights();

        Console.WriteLine("Enter flight id");
        if (!int.TryParse(Console.ReadLine(), out var flightId) || !flights.ContainsKey(flightId))
        {
            Console.WriteLine("Invalid flight id");
            return;
        }

        AppMenu.ShowFlightClassesMenu();

        if (!int.TryParse(Console.ReadLine(), out var classInput) || classInput < 1 || classInput > 3)
        {
            Console.WriteLine("Invalid class selection");
            return;
        }

        var flightClass = classInput switch
        {
            1 => FlightClasses.Economy,
            2 => FlightClasses.Business,
            3 => FlightClasses.First,
            _ => FlightClasses.Economy
        };

        Console.WriteLine("Enter date in yyyy-mm-dd hh:mm");
        if (!DateTime.TryParse(Console.ReadLine(), out var date))
        {
            Console.WriteLine("Invalid date format");
            return;
        }

        var flight = flights[flightId];

        var booking = Booking.Create(user, flight, flightClass, date);
        var bookingResult = await _bookingService.AddAsync(booking);

        if (bookingResult.IsFailure)
        {
            Console.WriteLine(bookingResult.Error);
            return;
        }

        Console.WriteLine("Booking successful");
    }

    private static async Task CancelBooking(User user)
    {
        Console.WriteLine("Cancel my booking");

        var bookings = FilteringHelper.GetFilteredEntitiesDictionary(
            _bookingService,
            Booking => Booking.Passenger.Id == user.Id);

        Console.WriteLine("Enter booking id");

        if (!int.TryParse(Console.ReadLine(), out var bookingId) || !bookings.ContainsKey(bookingId))
        {
            Console.WriteLine("Invalid booking id");
            return;
        }

        var bookingToDelete = bookings[bookingId];
        var deleteResult = await _bookingService.DeleteAsync(bookingToDelete.Id);

        if (deleteResult.IsFailure)
        {
            Console.WriteLine(deleteResult.Error);
            return;
        }

        Console.WriteLine("Booking cancelled");
    }
    private static void ViewBookings(User user)
    {
        Console.WriteLine("View my bookings");
        var myBookings = FilteringHelper.GetFilteredEntitiesDictionary(
            _bookingService,
            Booking => Booking.Passenger.Id == user.Id);

        foreach (var b in myBookings.Values)
        {
            Console.WriteLine($"Booking Id: {b.Id}");
            Console.WriteLine($"Flight Id: {b.Flight.FlightId}");
            Console.WriteLine($"Class: {b.ClassInfo.ClassName}");
            Console.WriteLine($"Passenger Id: {b.Passenger.Id}");
            Console.WriteLine("====================================");
        }
    }

    private static Dictionary<int, Flight> ListFlights()
    {
        var flightsResult = _flightService.GetAll();

        if (flightsResult.IsFailure)
        {
            Console.WriteLine(flightsResult.Error);
            return new Dictionary<int, Flight>();
        }

        var index = 1;
        var flights = flightsResult.Value;

        var flightDict = flights.ToDictionary(flight => index++, flight => flight);

        foreach (var flight in flightDict)
        {
            Console.WriteLine($"{flight.Key}. {flight.Value}");
        }

        return flightDict;
    }
}
