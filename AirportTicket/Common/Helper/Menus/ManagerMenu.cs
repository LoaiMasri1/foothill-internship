using AirportTicket.Common.Services.Impl;
using AirportTicket.Core.Configuration;
using AirportTicket.Features.Bookings.Services;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Flights.Services;
using AirportTicket.Features.Users.Services;

namespace AirportTicket.Common.Helper.Menus;

public class ManagerMenu
{
    private static readonly FlightService _flightService = new();
    private static readonly ImportCSVFileService<Flight, FlightMap>
        _importCSVFileService = new();

    private static void ShowMenu()
    {
        Console.WriteLine("1. Add a flight");
        Console.WriteLine("2. Import from CSV");
        Console.WriteLine("3. View Validations Rules");
        Console.WriteLine("4. Filter Bookings");
        Console.WriteLine("5. Exit");
    }
    private static void ShowFeaturesMenu()
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

    public static async Task Handle()
    {
        while (true)
        {
            ShowMenu();
            var managerInput = Console.ReadLine();

            switch (managerInput)
            {
                case "1":
                    await AddFlight();
                    break;
                case "2":
                    await ImportFlightsFromCSV();
                    break;
                case "3":
                    HandleFeatures();
                    break;
                case "4":
                    HandleBookingFilterring();
                    break;
                case "5":
                    AppMenu.Exit();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }
    private static async Task AddFlight()
    {
        Console.WriteLine("Write a departure country");
        var departureCountry = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Write a departure airport");
        var departureAirport = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Write a departure date in yyyy-mm-dd hh:mm");
        if (!DateTime.TryParse(Console.ReadLine(), out var departureDate))
        {
            Console.WriteLine("Invalid date format");
            return;
        }

        Console.WriteLine("Write a destination country");
        var destinationCountry = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Write a arrival airport");
        var destinationAirport = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Write available seats");
        if (!int.TryParse(Console.ReadLine(), out var availableSeats) || availableSeats <= 0)
        {
            Console.WriteLine("Invalid available seats");
            return;
        }

        Console.WriteLine("Write a price");
        if (!decimal.TryParse(Console.ReadLine(), out var price) || price <= 0)
        {
            Console.WriteLine("Invalid price");
            return;
        }

        var departure = new Departure
        {
            Id = Guid.NewGuid(),
            Country = departureCountry,
            Airport = departureAirport,
            Date = departureDate
        };

        var flight = new Flight
        {
            FlightId = Guid.NewGuid(),
            Departure = departure,
            Destination = new Destination
            {
                Country = destinationCountry,
                Airport = destinationAirport
            },
            AvailableSeats = availableSeats,
            Price = price
        };

        var flightResult = await _flightService.AddAsync(flight);

        if (flightResult.IsFailure)
        {
            Console.WriteLine(flightResult.Error);
            return;
        }

        Console.WriteLine($"Flight {flight.FlightId} added successfully");
    }

    private static async Task ImportFlightsFromCSV()
    {
        Console.WriteLine("Import from CSV");
        Console.WriteLine("Enter File Name");
        Console.WriteLine("Note: you can use the sample file in the Documents/Storage folder");
        System.Console.WriteLine("And please place the file in the Documents/Storage folder");


        var fileName = Console.ReadLine() ?? string.Empty;
        var csvResult = await _importCSVFileService.ImportAsync(fileName, _flightService);

        if (csvResult.IsFailure)
        {
            Console.WriteLine(csvResult.Error);
            return;
        }

        Console.WriteLine("Import successful");
    }
    private static void HandleFeatures()
    {
        while (true)
        {
            ShowFeaturesMenu();
            var featureInput = Console.ReadLine();

            switch (featureInput)
            {
                case "1":
                    FlightService.GetValidationRules().ForEach(Console.WriteLine);
                    break;
                case "2":
                    BookingService.GetValidationRules().ForEach(Console.WriteLine);
                    break;
                case "3":
                    UserService.GetValidationRules().ForEach(Console.WriteLine);
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
}
