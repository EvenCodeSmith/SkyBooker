using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightService.Models
{
    public class Flight
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("flightId")]
        public string FlightId { get; set; } = string.Empty;

        public string AirlineName { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;

        public DateTime Departure_Time { get; set; }
        public DateTime Arrival_Time { get; set; }

        public int Available_Seats { get; set; }
        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public DateTime Updated_At { get; set; } = DateTime.UtcNow;
    }
}
