using AirportTicket.Common.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AirportTicket.Common.Helper;

public class ValidationHelper
{
    public static bool TryValidateObject<T>(T obj, out ICollection<ValidationResult> results)
    {
        var validationContext = new ValidationContext(obj!, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(obj!, validationContext, results, true);
    }

    public static Result<T> Validate<T>(T obj, Func<string, Error> error)
    {
        if (!TryValidateObject(obj, out var validationResults))
        {
            var errorMessages = validationResults.Select(vr => vr.ErrorMessage);
            return Result<T>.Failure(error(
                string.Join(
                ",",
                errorMessages)
            ));
        }

        return Result<T>.Success(obj);
    }

    public static List<ValidationRule> GetValidationRules<T>() where T : class
    {
        var properties = typeof(T).GetProperties();
        var validationRules = new List<ValidationRule>();

        foreach (var property in properties)
        {
            var validationRule = new ValidationRule
            {
                PropertyName = property.Name,
                Rules =
                    property.GetCustomAttributes<ValidationAttribute>()
                    .Select(a => a.ErrorMessage)
                    .ToArray()!
            };

            if (validationRule.Rules.Length > 0)
            {
                validationRules.Add(validationRule);
            }
        }

        return validationRules;
    }
}