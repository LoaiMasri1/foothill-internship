using FluentValidation;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Validators;

public class ReservationValidator : AbstractValidator<ReservationRequest>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IResturantRepository _resturantRepository;
    private readonly ITableRepository _tableRepository;

    public ReservationValidator(
        ICustomerRepository customerRepository,
        IResturantRepository resturantRepository,
        ITableRepository tableRepository
    )
    {
        _customerRepository = customerRepository;
        _resturantRepository = resturantRepository;
        _tableRepository = tableRepository;

        RuleFor(x => x.CustomerId)
            .MustAsync(
                async (customerId, cancellation) =>
                {
                    var isExist = await _customerRepository.IsExistAsync(customerId);
                    return isExist;
                }
            )
            .WithMessage("Customer does not exist.");

        RuleFor(x => x.RestaurantId)
            .MustAsync(
                async (restaurantId, cancellation) =>
                {
                    var isExist = await _resturantRepository.IsExistAsync(restaurantId);
                    return isExist;
                }
            )
            .WithMessage("Restaurant does not exist.");

        RuleFor(x => x.TableId)
            .MustAsync(
                async (tableId, cancellation) =>
                {
                    var isExist = await _tableRepository.IsExistAsync(tableId);
                    return isExist;
                }
            )
            .WithMessage("Table does not exist.");

        RuleFor(x => x.ReservationDate).NotEmpty().WithMessage("Reservation date is required.");
    }
}
