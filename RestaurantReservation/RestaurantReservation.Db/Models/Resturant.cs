namespace RestaurantReservation.Db.Models;

public partial class Resturant
{
    public int ResturantsId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int PhoneNumber { get; set; }

    public string OpeningHours { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public ICollection<Table> Tables { get; set; } = new List<Table>();
}
