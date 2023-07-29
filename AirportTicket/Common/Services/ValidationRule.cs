namespace AirportTicket.Common.Services;

public class ValidationRule
{
    public string PropertyName { get; set; } = null!;
    public string[] Rules { get; set; } = null!;

    public override string ToString() => $"{PropertyName}: {string.Join(", ", Rules)}";
}