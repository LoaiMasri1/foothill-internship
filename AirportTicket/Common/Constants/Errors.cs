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
}
