using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateReservationView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW ReservationsView AS
            SELECT
                r.reservations_id AS ReservationId,
                r.reservation_date AS ReservationDate,
                r.party_size AS PartySize,
                c.customer_id AS CustomerId,
                c.first_name AS CustomerFirstName,
                c.last_name AS CustomerLastName,
                c.email AS CustomerEmail,
                r.resturant_id AS RestaurantId,
                rs.name AS RestaurantName,
                rs.address AS RestaurantAddress,
                rs.phone_number AS RestaurantPhoneNumber,
                rs.opening_hours AS RestaurantOpeningHours
            FROM Reservations r
            INNER JOIN Customers c ON r.customer_id = c.customer_id
            INNER JOIN Resturants rs ON r.resturant_id = rs.resturants_id;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW ReservationsView;");
        }
           
    }
}
