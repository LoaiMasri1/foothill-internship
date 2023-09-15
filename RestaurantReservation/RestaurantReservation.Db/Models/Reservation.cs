namespace RestaurantReservation.Db.Models;

public partial class Reservation
{
    public int ReservationsId { get; set; }

    public int CustomerId { get; set; }

    public int ResturantId { get; set; }

    public int TableId { get; set; }

    public DateTime ReservationDate { get; set; }

    public int PartySize { get; set; }

    public Customer Customer { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public Resturant Resturant { get; set; } = null!;

    public Table Table { get; set; } = null!;
}
