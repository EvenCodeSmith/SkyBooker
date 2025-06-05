using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; }
    }
}
