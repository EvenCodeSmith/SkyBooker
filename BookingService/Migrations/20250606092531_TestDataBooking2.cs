using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingService.Migrations
{
    /// <inheritdoc />
    public partial class TestDataBooking2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 6, 9, 25, 31, 320, DateTimeKind.Utc).AddTicks(6208), new DateTime(2025, 6, 6, 9, 25, 31, 320, DateTimeKind.Utc).AddTicks(6208) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 6, 9, 25, 31, 320, DateTimeKind.Utc).AddTicks(6212), new DateTime(2025, 6, 6, 9, 25, 31, 320, DateTimeKind.Utc).AddTicks(6212) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 6, 9, 24, 37, 430, DateTimeKind.Utc).AddTicks(1675), new DateTime(2025, 6, 6, 9, 24, 37, 430, DateTimeKind.Utc).AddTicks(1675) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 6, 9, 24, 37, 430, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 6, 6, 9, 24, 37, 430, DateTimeKind.Utc).AddTicks(1679) });
        }
    }
}
