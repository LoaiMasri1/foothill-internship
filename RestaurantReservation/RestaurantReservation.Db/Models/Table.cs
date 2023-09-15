namespace RestaurantReservation.Db.Models;

public partial class Table
{
    public int TableId { get; set; }

    public int ResturantId { get; set; }

    public int Capacity { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public Resturant Resturant { get; set; } = null!;
}
