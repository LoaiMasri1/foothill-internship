using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateRestaurantRevenueFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE FUNCTION CalculateRestaurantRevenue (@restaurantId INT)
            RETURNS DECIMAL(8, 2)
            AS
            BEGIN
                DECLARE @totalRevenue DECIMAL(8, 2)

                SELECT @totalRevenue = SUM(oi.quantity * mi.price)
                FROM OrderItems oi
                INNER JOIN Orders o ON oi.order_id = o.order_id
                INNER JOIN MenuItems mi ON oi.item_id = mi.item_id
                INNER JOIN Reservations r ON o.reservation_id = r.reservations_id
                WHERE r.resturant_id = @restaurantId

                RETURN @totalRevenue
            END
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS CalculateRestaurantRevenue");
        }
    }
}
