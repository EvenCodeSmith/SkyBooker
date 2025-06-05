using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public string FlightId { get; set; } = string.Empty;
        public string PassengerId { get; set; } = string.Empty;
        public string PassengerFirstname { get; set; } = string.Empty;
        public string PassengerLastname { get; set; } = string.Empty;
        public int TicketCount { get; set; }
        public string Username { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
