namespace AirportTicket.Common;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Value cannot be null");
    public static readonly Error NotExpected = new("Error.NotExpected", "Error not expected");

    public Error(string code, string errorMessage)
    {
        Code = code;
        ErrorMessage = errorMessage;
    }
    public string Code { get; }
    public string ErrorMessage { get; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;
    public static implicit operator string(Error error) => $"{error.Code}: {error.ErrorMessage}";
}
