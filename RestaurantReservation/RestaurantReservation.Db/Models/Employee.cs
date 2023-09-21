namespace RestaurantReservation.Db.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int ResturantId { get; set; }

    public string FirstName { get; set; } =string.Empty;

    public string LastName { get; set; } =string.Empty;

    public string Position { get; set; } =string.Empty;

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public Resturant Resturant { get; set; } = null!;
}
