namespace AirportTicket.Common.Constants;

public class Errors
{
    public partial class CSV
    {
        public static Error CSVNotValid(string message) =>
        new("CSV.NotValid", message);
    }
    public partial class User
    {
        public static readonly Error UserAlreadyExists =
        new("User.AlreadyExists", "User already exists");

        public static readonly Error UserNotFound =
        new("User.NotFound", "User not found");

        public static readonly Error UserNotCreated =
        new("User.NotCreated", "User not created");

        public static readonly Error UserNotUpdated =
        new("User.NotUpdated", "User not updated");

        public static Error UserNotValid(string message) =>
            new("User.NotValid", message);
    }

    public partial class Flight
    {
        public static readonly Error FlightAlreadyExists =
        new("Flight.AlreadyExists", "Flight already exists");

        public static readonly Error FlightNotFound =
        new("Flight.NotFound", "Flight not found");

        public static readonly Error FlightNotCreated =
        new("Flight.NotCreated", "Flight not created");

        public static readonly Error FlightNotUpdated =
        new("Flight.NotUpdated", "Flight not updated");

        public static readonly Error NoAvailableSeats =
        new("Flight.NoAvailableSeats", "No available seats");

        public static Error FlightNotValid(string message) =>
        new("Flight.NotValid", message);
    }

    public partial class Deprature
    {
        public static Error DepratureNotValid(string message) =>
        new("Deprature.NotValid", message);
    }

    public partial class Destination
    {
        public static Error DestinationNotValid(string message) =>
        new("Destination.NotValid", message);
    }
}
