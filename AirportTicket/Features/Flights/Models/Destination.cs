using System.ComponentModel.DataAnnotations;

namespace AirportTicket.Features.Flights.Models;

public class Destination
{
    [Required(ErrorMessage = "Country is required.")]
    public string Country { get; set; } = null!;

    [Required(ErrorMessage = "Airport is required.")]
    public string Airport { get; set; } = null!;
}
