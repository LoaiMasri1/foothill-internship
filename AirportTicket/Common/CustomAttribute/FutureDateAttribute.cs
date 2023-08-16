using System.ComponentModel.DataAnnotations;

namespace AirportTicket.Common.CustomAttribute;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is not DateTime dateValue)
        {
            return new ValidationResult("Invalid date format.");
        }
        if (dateValue >= DateTime.UtcNow)
        {
            return ValidationResult.Success!;
        }

        return new ValidationResult("Date should be in the future.");
    }
}
