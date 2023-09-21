using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db;

public class RestaurantReservationDbContext : DbContext
{
    private const string DbConnectionString =
        "Server=localhost;Database=RestaurantReservationCore;User ID=sa;Password=yourStrong(!)Password;TrustServerCertificate=True;";

    public RestaurantReservationDbContext() { }

    public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<Resturant> Resturants { get; set; }

    public DbSet<Table> Tables { get; set; }
    public DbSet<ReservationsView> ReservationsViews { get; set; }

    public decimal CalculateRestaurantRevenue(int restaurantId) =>
        throw new NotSupportedException();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            DbConnectionString,
            options =>
                options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                )
        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Email).HasMaxLength(255).IsUnicode(false).HasColumnName("email");
            entity
                .Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity
                .Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity
                .Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity
                .Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity
                .Property(e => e.Position)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.ResturantId).HasColumnName("resturant_id");

            entity
                .HasOne(d => d.Resturant)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.ResturantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity
                .Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name).HasMaxLength(255).IsUnicode(false).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)").HasColumnName("price");
            entity.Property(e => e.ResturantId).HasColumnName("resturant_id");

            entity
                .HasOne(d => d.Resturant)
                .WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.ResturantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.OrderDate).HasColumnType("date").HasColumnName("order_date");
            entity.Property(e => e.ReservationId).HasColumnName("reservation_id");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");

            entity
                .HasOne(d => d.Employee)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_employee_id_foreign");

            entity
                .HasOne(d => d.Reservation)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId);

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity
                .HasOne(d => d.Item)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity
                .HasOne(d => d.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationsId);

            entity.Property(e => e.ReservationsId).HasColumnName("reservations_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.PartySize).HasColumnName("party_size");
            entity
                .Property(e => e.ReservationDate)
                .HasColumnType("date")
                .HasColumnName("reservation_date");
            entity.Property(e => e.ResturantId).HasColumnName("resturant_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity
                .HasOne(d => d.Customer)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity
                .HasOne(d => d.Resturant)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ResturantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity
                .HasOne(d => d.Table)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Resturant>(entity =>
        {
            entity.HasKey(e => e.ResturantsId);

            entity.Property(e => e.ResturantsId).HasColumnName("resturants_id");
            entity
                .Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Name).HasMaxLength(255).IsUnicode(false).HasColumnName("name");
            entity
                .Property(e => e.OpeningHours)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("opening_hours");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId);

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.ResturantId).HasColumnName("resturant_id");

            entity
                .HasOne(d => d.Resturant)
                .WithMany(p => p.Tables)
                .HasForeignKey(d => d.ResturantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ReservationsView>().HasNoKey().ToView(nameof(ReservationsView));

        modelBuilder
            .HasDbFunction(
                typeof(RestaurantReservationDbContext).GetMethod(
                    nameof(CalculateRestaurantRevenue),
                    new[] { typeof(int) }
                )!
            )
            .HasName("CalculateRestaurantRevenue");

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Customer>()
            .HasData(
                new Customer
                {
                    CustomerId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@test.com"
                },
                new Customer
                {
                    CustomerId = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane@test.com"
                },
                new Customer
                {
                    CustomerId = 3,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Email = "bob@tst.com"
                },
                new Customer
                {
                    CustomerId = 4,
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "Alice@tst.uk"
                },
                new Customer
                {
                    CustomerId = 5,
                    FirstName = "Tom",
                    LastName = "Jones",
                    Email = "tom@tst.ps"
                }
            );

        modelBuilder
            .Entity<Resturant>()
            .HasData(
                new Resturant
                {
                    ResturantsId = 1,
                    Name = "The Restaurant",
                    Address = "123 Main Street",
                    PhoneNumber = 1234567890,
                    OpeningHours = "9:00 AM - 9:00 PM"
                },
                new Resturant
                {
                    ResturantsId = 2,
                    Name = "McDonalds",
                    Address = "123 Main Street",
                    PhoneNumber = 1234567890,
                    OpeningHours = "8:00 AM - 10:00 PM"
                },
                new Resturant
                {
                    ResturantsId = 3,
                    Name = "Burger King",
                    Address = "123 Main Street",
                    PhoneNumber = 1234567890,
                    OpeningHours = "8:00 AM - 10:00 PM"
                },
                new Resturant
                {
                    ResturantsId = 4,
                    Name = "Wendy's",
                    Address = "123 Main Street",
                    PhoneNumber = 1234567890,
                    OpeningHours = "8:00 AM - 10:00 PM"
                },
                new Resturant
                {
                    ResturantsId = 5,
                    Name = "Taco Bell",
                    Address = "123 Main Street",
                    PhoneNumber = 1234567890,
                    OpeningHours = "8:00 AM - 10:00 PM"
                }
            );

        modelBuilder
            .Entity<Table>()
            .HasData(
                new Table
                {
                    TableId = 1,
                    Capacity = 4,
                    ResturantId = 1
                },
                new Table
                {
                    TableId = 2,
                    Capacity = 4,
                    ResturantId = 1
                },
                new Table
                {
                    TableId = 3,
                    Capacity = 4,
                    ResturantId = 1
                },
                new Table
                {
                    TableId = 4,
                    Capacity = 4,
                    ResturantId = 1
                },
                new Table
                {
                    TableId = 5,
                    Capacity = 4,
                    ResturantId = 1
                }
            );

        modelBuilder
            .Entity<MenuItem>()
            .HasData(
                new MenuItem
                {
                    ItemId = 1,
                    Name = "Steak",
                    Description = "A 12 oz. cut of steak",
                    Price = 19.99m,
                    ResturantId = 1
                },
                new MenuItem
                {
                    ItemId = 2,
                    Name = "Chicken",
                    Description = "A 12 oz. chicken breast",
                    Price = 14.99m,
                    ResturantId = 1
                },
                new MenuItem
                {
                    ItemId = 3,
                    Name = "Pork",
                    Description = "A 12 oz. pork chop",
                    Price = 14.99m,
                    ResturantId = 1
                },
                new MenuItem
                {
                    ItemId = 4,
                    Name = "Hamburger",
                    Description = "A 1/2 lb. hamburger",
                    Price = 9.99m,
                    ResturantId = 1
                },
                new MenuItem
                {
                    ItemId = 5,
                    Name = "Cheeseburger",
                    Description = "A 1/2 lb. cheeseburger",
                    Price = 10.99m,
                    ResturantId = 1
                }
            );

        modelBuilder
            .Entity<Employee>()
            .HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Position = "Manager",
                    ResturantId = 1
                },
                new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Position = "Server",
                    ResturantId = 1
                },
                new Employee
                {
                    EmployeeId = 3,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Position = "Server",
                    ResturantId = 1
                },
                new Employee
                {
                    EmployeeId = 4,
                    FirstName = "Alice",
                    LastName = "Smith",
                    Position = "Server",
                    ResturantId = 1
                },
                new Employee
                {
                    EmployeeId = 5,
                    FirstName = "Tom",
                    LastName = "Jones",
                    Position = "Server",
                    ResturantId = 1
                }
            );

        modelBuilder
            .Entity<Reservation>()
            .HasData(
                new Reservation
                {
                    ReservationsId = 1,
                    CustomerId = 1,
                    ResturantId = 1,
                    TableId = 1,
                    PartySize = 4,
                    ReservationDate = DateTime.Now.AddDays(1)
                },
                new Reservation
                {
                    ReservationsId = 2,
                    CustomerId = 2,
                    ResturantId = 1,
                    TableId = 2,
                    PartySize = 4,
                    ReservationDate = DateTime.Now.AddDays(1)
                },
                new Reservation
                {
                    ReservationsId = 3,
                    CustomerId = 3,
                    ResturantId = 1,
                    TableId = 3,
                    PartySize = 4,
                    ReservationDate = DateTime.Now.AddDays(1)
                },
                new Reservation
                {
                    ReservationsId = 4,
                    CustomerId = 4,
                    ResturantId = 1,
                    TableId = 4,
                    PartySize = 4,
                    ReservationDate = DateTime.Now.AddDays(1)
                },
                new Reservation
                {
                    ReservationsId = 5,
                    CustomerId = 5,
                    ResturantId = 1,
                    TableId = 5,
                    PartySize = 4,
                    ReservationDate = DateTime.Now.AddDays(1)
                }
            );

        modelBuilder
            .Entity<Order>()
            .HasData(
                new Order
                {
                    OrderId = 1,
                    EmployeeId = 1,
                    ReservationId = 1,
                    OrderDate = DateTime.Now.AddDays(1),
                    TotalAmount = 19
                },
                new Order
                {
                    OrderId = 2,
                    EmployeeId = 2,
                    ReservationId = 2,
                    OrderDate = DateTime.Now.AddDays(1),
                    TotalAmount = 14
                },
                new Order
                {
                    OrderId = 3,
                    EmployeeId = 3,
                    ReservationId = 3,
                    OrderDate = DateTime.Now.AddDays(1),
                    TotalAmount = 14
                },
                new Order
                {
                    OrderId = 4,
                    EmployeeId = 4,
                    ReservationId = 4,
                    OrderDate = DateTime.Now.AddDays(1),
                    TotalAmount = 9
                },
                new Order
                {
                    OrderId = 5,
                    EmployeeId = 5,
                    ReservationId = 5,
                    OrderDate = DateTime.Now.AddDays(1),
                    TotalAmount = 10
                }
            );

        modelBuilder
            .Entity<OrderItem>()
            .HasData(
                new OrderItem
                {
                    OrderItemId = 1,
                    OrderId = 1,
                    ItemId = 1,
                    Quantity = 1
                },
                new OrderItem
                {
                    OrderItemId = 2,
                    OrderId = 2,
                    ItemId = 2,
                    Quantity = 1
                },
                new OrderItem
                {
                    OrderItemId = 3,
                    OrderId = 3,
                    ItemId = 3,
                    Quantity = 1
                },
                new OrderItem
                {
                    OrderItemId = 4,
                    OrderId = 4,
                    ItemId = 4,
                    Quantity = 1
                },
                new OrderItem
                {
                    OrderItemId = 5,
                    OrderId = 5,
                    ItemId = 5,
                    Quantity = 1
                }
            );
    }
}
