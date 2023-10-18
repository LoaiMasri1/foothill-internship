using FluentValidation;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Validators;

public class OrderItemValidator : AbstractValidator<OrderItemRequest>
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemValidator(
        IMenuItemRepository menuItemRepository,
        IOrderItemRepository orderItemRepository
    )
    {
        _menuItemRepository = menuItemRepository;
        _orderItemRepository = orderItemRepository;

        RuleFor(x => x.ItemId)
            .MustAsync(
                async (menuItemId, cancellation) =>
                {
                    var isExist = await _menuItemRepository.IsExistAsync(menuItemId);
                    return isExist;
                }
            )
            .WithMessage("MenuItem does not exist.");

        RuleFor(x => x.OrderId)
            .MustAsync(
                async (orderId, cancellation) =>
                {
                    var isExist = await _orderItemRepository.IsExistAsync(orderId);
                    return isExist;
                }
            )
            .WithMessage("Order does not exist.");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Quantity is required. Quantity must be greater than 0.");
    }
}
