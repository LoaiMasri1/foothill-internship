using AirportTicket.Features.Auth;
using AirportTicket.Features.Users.Models;
using AirportTicket.Features.Users.Models.Enums;
using System.Text;

namespace AirportTicket.Common.Helper.Menus;

public class AppMenu
{
    private static readonly AuthService authService = new();
    private static void Show()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");
    }
    public static void ShowFlightClassesMenu()
    {
        Console.WriteLine("Select Class");
        Console.WriteLine("1. Economy");
        Console.WriteLine("2. Business");
        Console.WriteLine("3. First Class");
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

    public static void Exit() => Environment.Exit(0);
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
            await PassengerMenu.Handle(user);
        }
        else if (user.Role == UserRole.Manager)
        {
            await ManagerMenu.Handle();
        }
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
