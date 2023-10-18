using FluentValidation;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Validators;

public class MenuItemValidator : AbstractValidator<MenuItemRequest>
{
    private readonly IResturantRepository _resturantRepository;

    public MenuItemValidator(IResturantRepository resturantRepository)
    {
        _resturantRepository = resturantRepository;

        RuleFor(x => x.RestaurantId)
            .MustAsync(
                async (resturantId, cancellation) =>
                {
                    var isExist = await _resturantRepository.IsExistAsync(resturantId);
                    return isExist;
                }
            )
            .WithMessage("Resturant does not exist.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required. Maximum length is 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required. Maximum length is 50 characters.");

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Price is required. Price must be greater than 0.");
    }
}
