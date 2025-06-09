using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    FlightId = "LX100",
                    PassengerId = "user-001",
                    PassengerFirstname = "Max",
                    PassengerLastname = "Muster",
                    TicketCount = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Booking
                {
                    Id = 2,
                    FlightId = "LH222",
                    PassengerId = "user-002",
                    PassengerFirstname = "Lisa",
                    PassengerLastname = "Meier",
                    TicketCount = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }

    }
}
