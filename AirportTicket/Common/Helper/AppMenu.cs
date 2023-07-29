using AirportTicket.Common.Services.Impl;
using AirportTicket.Core.Configuration;
using AirportTicket.Features.Auth;
using AirportTicket.Features.Bookings.Services;
using AirportTicket.Features.Flights.Models;
using AirportTicket.Features.Flights.Services;
using AirportTicket.Features.Users.Models;
using AirportTicket.Features.Users.Models.Enums;
using AirportTicket.Features.Users.Services;
using System.Text;

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

    public static async Task Handle()
    {
        while (true)
        {
            Show();
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await HandleLogin();
                    break;
                case "2":
                    await HandleRegistration();
                    break;
                case "3":
                    Exit();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private static void Exit() => Environment.Exit(0);
    private static async Task HandleLogin()
    {
        Console.WriteLine("Login");
        Console.WriteLine("Enter your email");
        var emailToLogin = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter your password");
        var passwordToLogin = Console.ReadLine() ?? string.Empty;

        var loginResult = await authService.LoginAsync(emailToLogin, passwordToLogin);

        if (loginResult.IsFailure)
        {
            Console.WriteLine(loginResult.Error);
            return;
        }

        var user = loginResult.Value;

        if (user.Role == UserRole.Passenger)
        {
            await HandlePassengerMenu(user);
        }
        else if (user.Role == UserRole.Manager)
        {
            await HandleManagerMenu();
        }
    }

    private static Task HandleManagerMenu()
    {
        throw new NotImplementedException();
    }

    private static Task HandlePassengerMenu(User user)
    {
        throw new NotImplementedException();
    }

    private static async Task HandleRegistration()
    {
        Console.WriteLine("Register");

        Console.WriteLine("Enter your email");
        var emailToRegister = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter your password");
        var passwordToRegister = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter your first name");
        var firstNameToRegister = Console.ReadLine();

        Console.WriteLine("Enter your last name");
        var lastNameToRegister = Console.ReadLine();

        Console.WriteLine("Select your role");
        Console.WriteLine("1. Passenger");
        Console.WriteLine("2. Manager");
        var roleInput = Console.ReadLine();

        if (!int.TryParse(
            roleInput,
            out var roleSelection) || roleSelection < 1 || roleSelection > 2)
        {
            Console.WriteLine("Invalid role selection");
            return;
        }

        var role = roleSelection switch
        {
            1 => UserRole.Passenger,
            2 => UserRole.Manager,
            _ => UserRole.Passenger
        };

        var sb = new StringBuilder();
        sb.Append(firstNameToRegister);
        sb.Append(' ');
        sb.Append(lastNameToRegister);

        var userToRegister = User.Create(
            sb.ToString(),
            passwordToRegister,
            emailToRegister,
            role);

        var registerResult = await authService.RegisterAsync(userToRegister);

        if (registerResult.IsFailure)
        {
            Console.WriteLine(registerResult.Error);
            return;
        }

        Console.WriteLine("Registration successful");
    }


}
