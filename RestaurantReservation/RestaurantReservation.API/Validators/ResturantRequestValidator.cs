using FluentValidation;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Validators;

public class ResturantRequestValidator : AbstractValidator<ResturantRequest>
{
    public ResturantRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name is required. Maximum length is 50 characters.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Address is required. Maximum length is 50 characters.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Address is required. Maximum length is 50 characters.");

        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");

        RuleFor(x => x.OpeningHours).NotEmpty().WithMessage("Opening hours is required.");
    }
}
