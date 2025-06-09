using BookingService.Controllers;
using BookingService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BookingService.Tests
{
    public class BookingControllerTests
    {
        [Fact]
        public async Task GetBookings_ReturnsListOfBookings()
        {
            var options = new DbContextOptionsBuilder<BookingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Bookings")
                .Options;

            using var context = new BookingDbContext(options);

            context.Bookings.Add(new Booking
            {
                Id = 1,
                FlightId = "LX123",
                PassengerId = "user-1",
                PassengerFirstname = "Max",
                PassengerLastname = "Meier",
                TicketCount = 2
            });

            context.Bookings.Add(new Booking
            {
                Id = 2,
                FlightId = "LX456",
                PassengerId = "user-2",
                PassengerFirstname = "Lisa",
                PassengerLastname = "Muster",
                TicketCount = 1
            });

            await context.SaveChangesAsync();

            var controller = new BookingController(context);

            var result = await controller.GetBookings();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var bookings = Assert.IsType<List<Booking>>(okResult.Value);
            Assert.Equal(2, bookings.Count);
        }

        [Fact]
        public async Task GetBooking_ReturnsCorrectBookingForUser()
        {
            var options = new DbContextOptionsBuilder<BookingDbContext>()
                .UseInMemoryDatabase(databaseName: "GetBookingDb")
                .Options;

            using var context = new BookingDbContext(options);

            context.Bookings.Add(new Booking
            {
                Id = 1,
                FlightId = "LX1",
                PassengerId = "user-123",
                PassengerFirstname = "Max",
                PassengerLastname = "Muster",
                TicketCount = 1
            });

            context.Bookings.Add(new Booking
            {
                Id = 2,
                FlightId = "LH2",
                PassengerId = "user-999",
                PassengerFirstname = "Lisa",
                PassengerLastname = "Meier",
                TicketCount = 2
            });

            await context.SaveChangesAsync();

            var controller = new BookingController(context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user-123")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await controller.GetBooking(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var booking = Assert.IsType<Booking>(okResult.Value);
            Assert.Equal("user-123", booking.PassengerId);
            Assert.Equal("LX1", booking.FlightId);
        }

        [Fact]
        public async Task CreateBooking_SavesBookingWithCorrectUser()
        {
            var options = new DbContextOptionsBuilder<BookingDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateBookingDb")
                .Options;

            using var context = new BookingDbContext(options);

            var controller = new BookingController(context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user-123")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var booking = new Booking
            {
                FlightId = "LX999",
                PassengerFirstname = "Test",
                PassengerLastname = "User",
                TicketCount = 1
            };

            var result = await controller.CreateBooking(booking);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var savedBooking = Assert.IsType<Booking>(createdResult.Value);
            Assert.Equal("user-123", savedBooking.PassengerId);

            var dbBooking = await context.Bookings.FirstOrDefaultAsync(b => b.FlightId == "LX999");
            Assert.NotNull(dbBooking);
            Assert.Equal("user-123", dbBooking!.PassengerId);
        }
        [Fact]
        public async Task CancelBooking_DeletesOwnBookingOnly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookingDbContext>()
                .UseInMemoryDatabase("CancelBookingDb")
                .Options;

            using var context = new BookingDbContext(options);

            context.Bookings.AddRange(
                new Booking
                {
                    Id = 1,
                    FlightId = "LX1",
                    PassengerId = "user-123",
                    PassengerFirstname = "Test",
                    PassengerLastname = "User",
                    TicketCount = 1
                },
                new Booking
                {
                    Id = 2,
                    FlightId = "LX2",
                    PassengerId = "other-user",
                    PassengerFirstname = "Anderer",
                    PassengerLastname = "User",
                    TicketCount = 1
                }
            );

            await context.SaveChangesAsync();

            var controller = new BookingController(context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.NameIdentifier, "user-123")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.CancelBooking(1);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var deletedBooking = await context.Bookings.FindAsync(1);
            var otherBooking = await context.Bookings.FindAsync(2);

            Assert.Null(deletedBooking);            // gelöscht
            Assert.NotNull(otherBooking);           // existiert noch
        }

    }
}
