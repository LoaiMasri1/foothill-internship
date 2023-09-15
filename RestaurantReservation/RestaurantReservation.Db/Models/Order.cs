namespace RestaurantReservation.Db.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int ReservationId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime OrderDate { get; set; }

    public int TotalAmount { get; set; }

    public Employee Employee { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public Reservation Reservation { get; set; } = null!;
}
