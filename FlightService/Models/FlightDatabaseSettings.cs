namespace FlightService.Models
{
    public class FlightDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string FlightCollectionName { get; set; } = null!;
    }
}
