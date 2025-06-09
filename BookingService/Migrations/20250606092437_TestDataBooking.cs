using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingService.Migrations
{
    /// <inheritdoc />
    public partial class TestDataBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedAt", "FlightId", "PassengerFirstname", "PassengerId", "PassengerLastname", "TicketCount", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 6, 9, 24, 37, 430, DateTimeKind.Utc).AddTicks(1675), "LX100", "Max", "user-001", "Muster", 1, new DateTime(2025, 6, 6, 9, 24, 37, 430, DateTimeKind.Utc).AddTicks(1675) },
                    { 2, new DateTime(2025, 6, 6, 9, 24, 37, 430, DateTimeKind.Utc).AddTicks(1679), "LH222", "Lisa", "user-002", "Meier", 2, new DateTime(2025, 6, 6, 9, 24, 37, 430, DateTimeKind.Utc).AddTicks(1679) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
