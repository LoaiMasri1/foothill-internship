using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordForCustomerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 1,
                column: "Password",
                value: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 2,
                column: "Password",
                value: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 3,
                column: "Password",
                value: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 4,
                column: "Password",
                value: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 5,
                column: "Password",
                value: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 1,
                column: "order_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5144));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 2,
                column: "order_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5150));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 3,
                column: "order_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5154));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 4,
                column: "order_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5159));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 5,
                column: "order_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5164));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 1,
                column: "reservation_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5040));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 2,
                column: "reservation_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5096));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 3,
                column: "reservation_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5101));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 4,
                column: "reservation_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5105));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 5,
                column: "reservation_date",
                value: new DateTime(2023, 10, 20, 16, 11, 2, 860, DateTimeKind.Local).AddTicks(5110));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 1,
                column: "order_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2568));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 2,
                column: "order_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2575));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 3,
                column: "order_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2579));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 4,
                column: "order_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2582));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 5,
                column: "order_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2585));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 1,
                column: "reservation_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2468));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 2,
                column: "reservation_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 3,
                column: "reservation_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 4,
                column: "reservation_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2535));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservations_id",
                keyValue: 5,
                column: "reservation_date",
                value: new DateTime(2023, 9, 18, 1, 21, 22, 763, DateTimeKind.Local).AddTicks(2538));
        }
    }
}
