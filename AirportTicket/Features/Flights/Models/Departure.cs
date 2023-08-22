using AirportTicket.Common.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace AirportTicket.Features.Flights.Models;

public class Departure
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    public string Country { get; set; } = null!;

    [Required(ErrorMessage = "Date is required.")]
    [FutureDate(ErrorMessage = "Date should be in the future.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Airport is required.")]
    public string Airport { get; set; } = null!;
}
