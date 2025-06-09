using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingService.Migrations
{
    /// <inheritdoc />
    public partial class TestDataBooking3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedAt", "FlightId", "PassengerFirstname", "PassengerId", "PassengerLastname", "TicketCount", "UpdatedAt" },
                values: new object[,]
                {
                    { 7, new DateTime(2025, 6, 6, 9, 27, 4, 430, DateTimeKind.Utc).AddTicks(1386), "LX100", "Max", "user-001", "Muster", 1, new DateTime(2025, 6, 6, 9, 27, 4, 430, DateTimeKind.Utc).AddTicks(1387) },
                    { 8, new DateTime(2025, 6, 6, 9, 27, 4, 430, DateTimeKind.Utc).AddTicks(1390), "LH222", "Lisa", "user-002", "Meier", 2, new DateTime(2025, 6, 6, 9, 27, 4, 430, DateTimeKind.Utc).AddTicks(1391) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedAt", "FlightId", "PassengerFirstname", "PassengerId", "PassengerLastname", "TicketCount", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 6, 9, 25, 31, 320, DateTimeKind.Utc).AddTicks(6208), "LX100", "Max", "user-001", "Muster", 1, new DateTime(2025, 6, 6, 9, 25, 31, 320, DateTimeKind.Utc).AddTicks(6208) },
                    { 2, new DateTime(2025, 6, 6, 9, 25, 31, 320, DateTimeKind.Utc).AddTicks(6212), "LH222", "Lisa", "user-002", "Meier", 2, new DateTime(2025, 6, 6, 9, 25, 31, 320, DateTimeKind.Utc).AddTicks(6212) }
                });
        }
    }
}
