using AirportTicket.Common.Services.Impl;
using AirportTicket.Core.Configuration;
using AirportTicket.Features.Auth;
using AirportTicket.Features.Bookings.Services;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Flights.Services;
using AirportTicket.Features.Users.Services;

namespace AirportTicket.Common.Helper;

public class AppMenu
{
    private static readonly AuthService authService = new();
    private static readonly BookingService bookingService = new();
    private static readonly FlightService flightService = new();
    private static readonly UserService userService = new();
    private static readonly ImportCSVFileService<Flight, FlightMap> importCSVFileService = new();
    private static void Show()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");
    }

    private static void ShowPassengerMenu()
    {
        Console.WriteLine("1. Book a flight");
        Console.WriteLine("2. Cancel a flight");
        Console.WriteLine("3. View my bookings");
        Console.WriteLine("4. Exit");
    }

    private static void ShowManagerMenu()
    {
        Console.WriteLine("1. Add a flight");
        Console.WriteLine("2. Import from CSV");
        Console.WriteLine("3. View Validations Rules");
        Console.WriteLine("4. Filter Bookings");
        Console.WriteLine("5. Exit");
    }

    private static void ShowFeatures()
    {
        Console.WriteLine("1. Flight");
        Console.WriteLine("2. Booking");
        Console.WriteLine("3. User");
        Console.WriteLine("4. Exit");
    }

    private static void ShowFilterBookingMenu()
    {
        Console.WriteLine("Filter Bookings");
        Console.WriteLine("1. By Flight");
        Console.WriteLine("2. By Passenger");
        Console.WriteLine("3. By Departure Country");
        Console.WriteLine("4. By Destination Country");
        Console.WriteLine("5. By Departure Date");
        Console.WriteLine("6. By Departure Airport");
        Console.WriteLine("7. By Destination Airport");
        Console.WriteLine("8. By Flight Class Type");
        Console.WriteLine("9. Exit");
    }
}
