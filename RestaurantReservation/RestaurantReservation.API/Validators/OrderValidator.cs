using FluentValidation;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Validators;

public class OrderValidator : AbstractValidator<OrderRequest>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public OrderValidator(
        IReservationRepository reservationRepository,
        IEmployeeRepository employeeRepository
    )
    {
        _reservationRepository = reservationRepository;
        _employeeRepository = employeeRepository;

        RuleFor(x => x.ReservationId)
            .MustAsync(
                async (reservationId, cancellation) =>
                {
                    var isExist = await _reservationRepository.IsExistAsync(reservationId);
                    return isExist;
                }
            )
            .WithMessage("Reservation does not exist.");

        RuleFor(x => x.EmployeeId)
            .MustAsync(
                async (employeeId, cancellation) =>
                {
                    var isExist = await _employeeRepository.IsExistAsync(employeeId);
                    return isExist;
                }
            )
            .WithMessage("Employee does not exist.");

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .Must(x => x > DateTime.Now)
            .WithMessage("Order date is required. Order date must be greater than current date.");
    }
}
