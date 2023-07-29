namespace AirportTicket.Common.Constants;

public class Errors
{
    public partial class CSV
    {
        public static Error CSVNotValid(string message) =>
        new("CSV.NotValid", message);
    }
}
