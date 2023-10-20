using FluentValidation;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Validators;

public class TableRequestValidator : AbstractValidator<TableRequest>
{
    private readonly IResturantRepository _resturantRepository;

    public TableRequestValidator(IResturantRepository resturantRepository)
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

        RuleFor(x => x.Capacity)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Capacity is required. Capacity must be greater than 0.");
    }
}
