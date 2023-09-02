using AirportTicket.Common.Constants;
using AirportTicket.Common.Models;
using AirportTicket.Common.Services.Impl;
using AirportTicket.Common.Wrappers;
using AirportTicket.Core;
using AirportTicket.Core.Configuration;
using AirportTicket.Features.Bookings.Services;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Flights.Services;
using AirportTicket.Features.Users.Models.Enums;
using AirportTicket.Features.Users.Services;

namespace AirportTicket.Common.Helper.Menus;

public class ManagerMenu
{
    private static readonly IStorage _storage = MongoStorage.Instance;
    private static readonly ICSVReader _csvReader = new CSVReader();
    private static readonly FlightService _flightService = new(_storage);
    private static readonly ImportCSVFileService<Flight, FlightMap>
        _importCSVFileService = new(_csvReader);
    private static readonly IBookingService _bookingService = new BookingService(_flightService, _storage);
    private static readonly UserService _userService = new(_storage);

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
                    HandleBookingFiltering();
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
        Console.WriteLine("Note: you can take a look for sample file in the Project folder");
        System.Console.WriteLine("And please place your csv file in the Documents/Storage folder");


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

    private static void HandleBookingFiltering()
    {
        while (true)
        {
            ShowFilterBookingMenu();
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    FilterBookingsByFlight();
                    break;
                case "2":
                    FilterBookingsByUser();
                    break;

                case "3":
                    FilterBookingsByDepartureCountry();
                    break;

                case "4":
                    FilterBookingsByDestinationCountry();
                    break;

                case "5":
                    FilterBookingsByDepartureDate();
                    break;

                case "6":
                    FilterBookingsByDepartureAirport();
                    break;

                case "7":
                    FilterBookingsByDestinationAirport();
                    break;

                case "8":
                    FilterBookingsByFlightClassType();
                    break;

                case "9":
                    AppMenu.Exit();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private static void FilterBookingsByFlightClassType()
    {
        var bookings = FilteringHelper.GetFilteredEntitiesDictionary(_bookingService);

        var flightClassTypes = bookings.Select(b => b.Value.ClassInfo)
            .Distinct()
            .ToList();

        flightClassTypes.ForEach(Console.WriteLine);

        AppMenu.ShowFlightClassesMenu();

        Console.WriteLine("Select flight class type");
        var flightClass = int.TryParse(Console.ReadLine(), out var flightClassType)
            ? flightClassType
            : 1;

        var classType = flightClass switch
        {
            1 => FlightClasses.Economy,
            2 => FlightClasses.Business,
            3 => FlightClasses.First,
            _ => FlightClasses.Economy,
        };

        Console.WriteLine($"Bookings for {classType} class");

        var filteredBookings = bookings
            .Where(b => b.Value.ClassInfo.ClassName == classType.ClassName)
            .ToList();

        filteredBookings.ForEach(b => Console.WriteLine(
            $"Id: {b.Key}, " +
            $"Flight: {b.Value.Flight}, " +
            $"Passenger: {b.Value.Passenger}, " +
            $"Class: {b.Value.ClassInfo}"));
    }

    private static void FilterBookingsByDestinationAirport()
    {
        var bookings = FilteringHelper.GetFilteredEntitiesDictionary(_bookingService);

        var destinationAirports = bookings.Select(b => b.Value.Flight.Destination.Airport)
            .Distinct()
            .ToList();

        destinationAirports.ForEach(Console.WriteLine);

        Console.WriteLine("Enter destination airport");
        var destinationAirport = Console.ReadLine();

        var filteredBookings = bookings
            .Where(b => b.Value.Flight.Destination.Airport == destinationAirport);

        filteredBookings.ToList().ForEach(b => Console.WriteLine(
            $"Id: {b.Key}, Flight: {b.Value.Flight}, User: {b.Value.Passenger}"));

    }

    private static void FilterBookingsByDepartureAirport()
    {
        var bookings =
            FilteringHelper.GetFilteredEntitiesDictionary(_bookingService);

        var departureAirports = bookings.Select(b => b.Value.Flight.Departure.Airport)
            .Distinct()
            .ToList();

        departureAirports.ForEach(Console.WriteLine);

        Console.WriteLine("Enter departure airport");
        var departureAirport = Console.ReadLine();

        var filteredBookings = bookings
            .Where(b => b.Value.Flight.Departure.Airport == departureAirport);

        filteredBookings.ToList().ForEach(b => Console.WriteLine(
            $"Id: {b.Key}, Flight: {b.Value.Flight}, User: {b.Value.Passenger}"));

    }

    private static void FilterBookingsByDepartureDate()
    {
        var bookings = FilteringHelper.GetFilteredEntitiesDictionary(_bookingService);

        var departureDates = bookings.Select(b => b.Value.Flight.Departure.Date
                .ToString("dd/MM/yyyy HH:mm"))
            .Distinct()
            .ToList();

        departureDates.ForEach(Console.WriteLine);

        Console.WriteLine("Enter departure date");
        var departureDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

        var filteredBookings = bookings
            .Where(b => b.Value.Flight.Departure.Date == departureDate);

        filteredBookings.ToList().ForEach(b => Console.WriteLine(
            $"Id: {b.Key}, Flight: {b.Value.Flight}, User: {b.Value.Passenger}"));
    }

    private static void FilterBookingsByDestinationCountry()
    {

        var bookings = FilteringHelper.GetFilteredEntitiesDictionary(_bookingService);

        var countries = bookings.Select(b => b.Value.Flight.Destination.Country)
            .Distinct()
            .ToList();

        countries.ForEach(Console.WriteLine);

        Console.WriteLine("Enter country name");
        var countryName = Console.ReadLine();

        var filteredBookings = bookings
            .Where(b => b.Value.Flight.Destination.Country == countryName);

        filteredBookings.ToList().ForEach(b => Console.WriteLine(
            $"Id: {b.Key}, Flight: {b.Value.Flight}, User: {b.Value.Passenger}"));
    }

    private static void FilterBookingsByDepartureCountry()
    {
        var bookings = FilteringHelper.GetFilteredEntitiesDictionary(_bookingService);

        var countries = bookings.Select(b => b.Value.Flight.Departure.Country)
            .Distinct()
            .ToList();

        countries.ForEach(Console.WriteLine);

        Console.WriteLine("Enter country name");
        var countryName = Console.ReadLine();

        var filteredBookings = bookings
            .Where(b => b.Value.Flight.Departure.Country == countryName);

        filteredBookings.ToList().ForEach(b => Console.WriteLine(
            $"Id: {b.Key}, Flight: {b.Value.Flight}, User: {b.Value.Passenger}"));
    }
    private static void FilterBookingsByUser()
    {
        var users = FilteringHelper.GetFilteredEntitiesDictionary(
            _userService,
            user => user.Role == UserRole.Passenger);

        users.ToList().ForEach(u => Console.WriteLine(
            $"Id: {u.Key}, Name: {u.Value.Name}"));

        Console.WriteLine("Enter user id");
        if (!int.TryParse(Console.ReadLine(), out var userId) || !users.ContainsKey(userId))
        {
            Console.WriteLine("Invalid user id");
            return;
        }

        var user = users[userId];

        var bookingsResult = _bookingService.GetAll(Booking => Booking.Passenger.Id == user.Id);

        if (bookingsResult.IsFailure)
        {
            Console.WriteLine(bookingsResult.Error);
            return;
        }

        var bookings = bookingsResult.Value.ToList();

        if (bookings.Count == 0)
        {
            Console.WriteLine("No bookings found");
            return;
        }

        bookings.ForEach(Console.WriteLine);
    }


    private static void FilterBookingsByFlight()
    {
        var flights = FilteringHelper.GetFilteredEntitiesDictionary(_flightService);

        flights.ToList().ForEach(f => Console.WriteLine(
            $"Id: {f.Key}, Flight: {f.Value}"));

        Console.WriteLine("Enter flight id");
        if (!int.TryParse(Console.ReadLine(), out var flightId) || !flights.ContainsKey(flightId))
        {
            Console.WriteLine("Invalid flight id");
            return;
        }

        var flight = flights[flightId];

        var bookingsResult = _bookingService.GetAll(booking => booking.Flight.FlightId == flight.FlightId);

        if (bookingsResult.IsFailure)
        {
            Console.WriteLine(bookingsResult.Error);
            return;
        }

        var bookings = bookingsResult.Value.ToList();

        if (bookings.Count == 0)
        {
            Console.WriteLine("No bookings found");
            return;
        }

        bookings.ForEach(Console.WriteLine);
    }
}
