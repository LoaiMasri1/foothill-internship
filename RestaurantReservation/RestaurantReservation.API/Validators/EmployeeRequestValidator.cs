using FluentValidation;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Validators;

public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
{
    private readonly IResturantRepository _resturantRepository;

    public EmployeeRequestValidator(IResturantRepository resturantRepository)
    {
        _resturantRepository = resturantRepository;

        RuleFor(x => x.ResturantId)
            .MustAsync(
                async (resturantId, cancellation) =>
                {
                    var isExist = await _resturantRepository.IsExistAsync(resturantId);
                    return isExist;
                }
            )
            .WithMessage("Resturant does not exist.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required. Maximum length is 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required. Maximum length is 50 characters.");

        RuleFor(x => x.Position)
            .NotEmpty()
            .WithMessage("Position is required. Maximum length is 50 characters.");
    }
}
