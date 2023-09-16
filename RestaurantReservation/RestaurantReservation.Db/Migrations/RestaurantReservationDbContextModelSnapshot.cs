﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantReservation.Db;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    [DbContext(typeof(RestaurantReservationDbContext))]
    partial class RestaurantReservationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantReservation.Db.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("last_name");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int")
                        .HasColumnName("phone_number");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers", (string)null);

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Email = "john@test.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PhoneNumber = 0
                        },
                        new
                        {
                            CustomerId = 2,
                            Email = "jane@test.com",
                            FirstName = "Jane",
                            LastName = "Doe",
                            PhoneNumber = 0
                        },
                        new
                        {
                            CustomerId = 3,
                            Email = "bob@tst.com",
                            FirstName = "Bob",
                            LastName = "Smith",
                            PhoneNumber = 0
                        },
                        new
                        {
                            CustomerId = 4,
                            Email = "Alice@tst.uk",
                            FirstName = "Alice",
                            LastName = "Smith",
                            PhoneNumber = 0
                        },
                        new
                        {
                            CustomerId = 5,
                            Email = "tom@tst.ps",
                            FirstName = "Tom",
                            LastName = "Jones",
                            PhoneNumber = 0
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("employee_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("last_name");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("position");

                    b.Property<int>("ResturantId")
                        .HasColumnType("int")
                        .HasColumnName("resturant_id");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ResturantId");

                    b.ToTable("Employees", (string)null);

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            FirstName = "John",
                            LastName = "Doe",
                            Position = "Manager",
                            ResturantId = 1
                        },
                        new
                        {
                            EmployeeId = 2,
                            FirstName = "Jane",
                            LastName = "Doe",
                            Position = "Server",
                            ResturantId = 1
                        },
                        new
                        {
                            EmployeeId = 3,
                            FirstName = "Bob",
                            LastName = "Smith",
                            Position = "Server",
                            ResturantId = 1
                        },
                        new
                        {
                            EmployeeId = 4,
                            FirstName = "Alice",
                            LastName = "Smith",
                            Position = "Server",
                            ResturantId = 1
                        },
                        new
                        {
                            EmployeeId = 5,
                            FirstName = "Tom",
                            LastName = "Jones",
                            Position = "Server",
                            ResturantId = 1
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItem", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("item_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8, 2)")
                        .HasColumnName("price");

                    b.Property<int>("ResturantId")
                        .HasColumnType("int")
                        .HasColumnName("resturant_id");

                    b.HasKey("ItemId");

                    b.HasIndex("ResturantId");

                    b.ToTable("MenuItems", (string)null);

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            Description = "A 12 oz. cut of steak",
                            Name = "Steak",
                            Price = 19.99m,
                            ResturantId = 1
                        },
                        new
                        {
                            ItemId = 2,
                            Description = "A 12 oz. chicken breast",
                            Name = "Chicken",
                            Price = 14.99m,
                            ResturantId = 1
                        },
                        new
                        {
                            ItemId = 3,
                            Description = "A 12 oz. pork chop",
                            Name = "Pork",
                            Price = 14.99m,
                            ResturantId = 1
                        },
                        new
                        {
                            ItemId = 4,
                            Description = "A 1/2 lb. hamburger",
                            Name = "Hamburger",
                            Price = 9.99m,
                            ResturantId = 1
                        },
                        new
                        {
                            ItemId = 5,
                            Description = "A 1/2 lb. cheeseburger",
                            Name = "Cheeseburger",
                            Price = 10.99m,
                            ResturantId = 1
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("employee_id");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date")
                        .HasColumnName("order_date");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int")
                        .HasColumnName("reservation_id");

                    b.Property<int>("TotalAmount")
                        .HasColumnType("int")
                        .HasColumnName("total_amount");

                    b.HasKey("OrderId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Orders", (string)null);

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            EmployeeId = 1,
                            OrderDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8314),
                            ReservationId = 1,
                            TotalAmount = 19
                        },
                        new
                        {
                            OrderId = 2,
                            EmployeeId = 2,
                            OrderDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8319),
                            ReservationId = 2,
                            TotalAmount = 14
                        },
                        new
                        {
                            OrderId = 3,
                            EmployeeId = 3,
                            OrderDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8322),
                            ReservationId = 3,
                            TotalAmount = 14
                        },
                        new
                        {
                            OrderId = 4,
                            EmployeeId = 4,
                            OrderDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8324),
                            ReservationId = 4,
                            TotalAmount = 9
                        },
                        new
                        {
                            OrderId = 5,
                            EmployeeId = 5,
                            OrderDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8327),
                            ReservationId = 5,
                            TotalAmount = 10
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_item_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int")
                        .HasColumnName("item_id");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);

                    b.HasData(
                        new
                        {
                            OrderItemId = 1,
                            ItemId = 1,
                            OrderId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 2,
                            ItemId = 2,
                            OrderId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 3,
                            ItemId = 3,
                            OrderId = 3,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 4,
                            ItemId = 4,
                            OrderId = 4,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 5,
                            ItemId = 5,
                            OrderId = 5,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("reservations_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationsId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.Property<int>("PartySize")
                        .HasColumnType("int")
                        .HasColumnName("party_size");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("date")
                        .HasColumnName("reservation_date");

                    b.Property<int>("ResturantId")
                        .HasColumnType("int")
                        .HasColumnName("resturant_id");

                    b.Property<int>("TableId")
                        .HasColumnType("int")
                        .HasColumnName("table_id");

                    b.HasKey("ReservationsId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ResturantId");

                    b.HasIndex("TableId");

                    b.ToTable("Reservations", (string)null);

                    b.HasData(
                        new
                        {
                            ReservationsId = 1,
                            CustomerId = 1,
                            PartySize = 4,
                            ReservationDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8218),
                            ResturantId = 1,
                            TableId = 1
                        },
                        new
                        {
                            ReservationsId = 2,
                            CustomerId = 2,
                            PartySize = 4,
                            ReservationDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8273),
                            ResturantId = 1,
                            TableId = 2
                        },
                        new
                        {
                            ReservationsId = 3,
                            CustomerId = 3,
                            PartySize = 4,
                            ReservationDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8276),
                            ResturantId = 1,
                            TableId = 3
                        },
                        new
                        {
                            ReservationsId = 4,
                            CustomerId = 4,
                            PartySize = 4,
                            ReservationDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8279),
                            ResturantId = 1,
                            TableId = 4
                        },
                        new
                        {
                            ReservationsId = 5,
                            CustomerId = 5,
                            PartySize = 4,
                            ReservationDate = new DateTime(2023, 9, 17, 15, 44, 16, 235, DateTimeKind.Local).AddTicks(8281),
                            ResturantId = 1,
                            TableId = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.ReservationView", b =>
                {
                    b.Property<int>("ResturantsId")
                        .HasColumnType("int");

                    b.HasIndex("ResturantsId");

                    b.ToTable((string)null);

                    b.ToView("ReservationView", (string)null);
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Resturant", b =>
                {
                    b.Property<int>("ResturantsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("resturants_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResturantsId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("OpeningHours")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("opening_hours");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int")
                        .HasColumnName("phone_number");

                    b.HasKey("ResturantsId");

                    b.ToTable("Resturants", (string)null);

                    b.HasData(
                        new
                        {
                            ResturantsId = 1,
                            Address = "123 Main Street",
                            Name = "The Restaurant",
                            OpeningHours = "9:00 AM - 9:00 PM",
                            PhoneNumber = 1234567890
                        },
                        new
                        {
                            ResturantsId = 2,
                            Address = "123 Main Street",
                            Name = "McDonalds",
                            OpeningHours = "8:00 AM - 10:00 PM",
                            PhoneNumber = 1234567890
                        },
                        new
                        {
                            ResturantsId = 3,
                            Address = "123 Main Street",
                            Name = "Burger King",
                            OpeningHours = "8:00 AM - 10:00 PM",
                            PhoneNumber = 1234567890
                        },
                        new
                        {
                            ResturantsId = 4,
                            Address = "123 Main Street",
                            Name = "Wendy's",
                            OpeningHours = "8:00 AM - 10:00 PM",
                            PhoneNumber = 1234567890
                        },
                        new
                        {
                            ResturantsId = 5,
                            Address = "123 Main Street",
                            Name = "Taco Bell",
                            OpeningHours = "8:00 AM - 10:00 PM",
                            PhoneNumber = 1234567890
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("table_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TableId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int")
                        .HasColumnName("capacity");

                    b.Property<int>("ResturantId")
                        .HasColumnType("int")
                        .HasColumnName("resturant_id");

                    b.HasKey("TableId");

                    b.HasIndex("ResturantId");

                    b.ToTable("Tables", (string)null);

                    b.HasData(
                        new
                        {
                            TableId = 1,
                            Capacity = 4,
                            ResturantId = 1
                        },
                        new
                        {
                            TableId = 2,
                            Capacity = 4,
                            ResturantId = 1
                        },
                        new
                        {
                            TableId = 3,
                            Capacity = 4,
                            ResturantId = 1
                        },
                        new
                        {
                            TableId = 4,
                            Capacity = 4,
                            ResturantId = 1
                        },
                        new
                        {
                            TableId = 5,
                            Capacity = 4,
                            ResturantId = 1
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Resturant", "Resturant")
                        .WithMany("Employees")
                        .HasForeignKey("ResturantId")
                        .IsRequired();

                    b.Navigation("Resturant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItem", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Resturant", "Resturant")
                        .WithMany("MenuItems")
                        .HasForeignKey("ResturantId")
                        .IsRequired();

                    b.Navigation("Resturant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("orders_employee_id_foreign");

                    b.HasOne("RestaurantReservation.Db.Models.Reservation", "Reservation")
                        .WithMany("Orders")
                        .HasForeignKey("ReservationId")
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.OrderItem", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.MenuItem", "Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Resturant", "Resturant")
                        .WithMany("Reservations")
                        .HasForeignKey("ResturantId")
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Table", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Resturant");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.ReservationView", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Resturant", "Resturant")
                        .WithMany()
                        .HasForeignKey("ResturantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resturant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Resturant", "Resturant")
                        .WithMany("Tables")
                        .HasForeignKey("ResturantId")
                        .IsRequired();

                    b.Navigation("Resturant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItem", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Resturant", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("MenuItems");

                    b.Navigation("Reservations");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
