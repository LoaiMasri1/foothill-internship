using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class seeddatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "customer_id", "email", "first_name", "last_name", "phone_number" },
                values: new object[,]
                {
                    { 1, "john@test.com", "John", "Doe", 0 },
                    { 2, "jane@test.com", "Jane", "Doe", 0 },
                    { 3, "bob@tst.com", "Bob", "Smith", 0 },
                    { 4, "Alice@tst.uk", "Alice", "Smith", 0 },
                    { 5, "tom@tst.ps", "Tom", "Jones", 0 }
                });

            migrationBuilder.InsertData(
                table: "Resturants",
                columns: new[] { "resturants_id", "address", "name", "opening_hours", "phone_number" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "The Restaurant", "9:00 AM - 9:00 PM", 1234567890 },
                    { 2, "123 Main Street", "McDonalds", "8:00 AM - 10:00 PM", 1234567890 },
                    { 3, "123 Main Street", "Burger King", "8:00 AM - 10:00 PM", 1234567890 },
                    { 4, "123 Main Street", "Wendy's", "8:00 AM - 10:00 PM", 1234567890 },
                    { 5, "123 Main Street", "Taco Bell", "8:00 AM - 10:00 PM", 1234567890 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "employee_id", "first_name", "last_name", "position", "resturant_id" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "Manager", 1 },
                    { 2, "Jane", "Doe", "Server", 1 },
                    { 3, "Bob", "Smith", "Server", 1 },
                    { 4, "Alice", "Smith", "Server", 1 },
                    { 5, "Tom", "Jones", "Server", 1 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "item_id", "description", "name", "price", "resturant_id" },
                values: new object[,]
                {
                    { 1, "A 12 oz. cut of steak", "Steak", 19.99m, 1 },
                    { 2, "A 12 oz. chicken breast", "Chicken", 14.99m, 1 },
                    { 3, "A 12 oz. pork chop", "Pork", 14.99m, 1 },
                    { 4, "A 1/2 lb. hamburger", "Hamburger", 9.99m, 1 },
                    { 5, "A 1/2 lb. cheeseburger", "Cheeseburger", 10.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "table_id", "capacity", "resturant_id" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 4, 1 },
                    { 3, 4, 1 },
                    { 4, 4, 1 },
                    { 5, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "reservations_id", "customer_id", "party_size", "reservation_date", "resturant_id", "table_id" },
                values: new object[,]
                {
                    { 1, 1, 4, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8715), 1, 1 },
                    { 2, 2, 4, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8774), 1, 2 },
                    { 3, 3, 4, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8778), 1, 3 },
                    { 4, 4, 4, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8781), 1, 4 },
                    { 5, 5, 4, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8784), 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "order_id", "employee_id", "order_date", "reservation_id", "total_amount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8812), 1, 19 },
                    { 2, 2, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8819), 2, 14 },
                    { 3, 3, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8822), 3, 14 },
                    { 4, 4, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8825), 4, 9 },
                    { 5, 5, new DateTime(2023, 9, 17, 11, 47, 2, 867, DateTimeKind.Local).AddTicks(8828), 5, 10 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "order_item_id", "item_id", "order_id", "quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 1 },
                    { 3, 3, 3, 1 },
                    { 4, 4, 4, 1 },
                    { 5, 5, 5, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Resturants",
                keyColumn: "resturants_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resturants",
                keyColumn: "resturants_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Resturants",
                keyColumn: "resturants_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Resturants",
                keyColumn: "resturants_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Resturants",
                keyColumn: "resturants_id",
                keyValue: 1);
        }
    }
}
