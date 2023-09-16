using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateFindCustomersWithLargePartiesProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE FindCustomersWithLargeParties
                            @min_party_size INT
                            AS
                            BEGIN
                                WITH DistinctCustomers AS (
                                    SELECT DISTINCT C.*
                                    FROM Customers AS C
                                    JOIN Reservations AS R ON C.customer_id = R.customer_id
                                    WHERE R.party_size > @min_party_size
                                )
                                SELECT * FROM DistinctCustomers;
                            END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE FindCustomersWithLargeParties");
        }
    }
}
