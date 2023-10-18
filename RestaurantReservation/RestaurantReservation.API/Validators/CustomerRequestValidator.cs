using FluentValidation;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Validators;

public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
{
    public CustomerRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required. Maximum length is 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required. Maximum length is 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required. Email address must be in valid format.");

        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
    }
}
