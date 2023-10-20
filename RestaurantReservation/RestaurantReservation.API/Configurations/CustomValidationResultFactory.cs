namespace RestaurantReservation.API.Configurations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using System.Collections.Generic;
using System.Net;

public class CustomValidationResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        var errors = validationProblemDetails?.Errors;

        return new BadRequestObjectResult(
            new ValidationErrorDetails(
                errors,
                (int)HttpStatusCode.BadRequest,
                "Validation Error Occurs"));
    }
}

internal record ValidationErrorDetails(
    IDictionary<string, string[]>? Errors,
    int StatusCode,
    string Message);
