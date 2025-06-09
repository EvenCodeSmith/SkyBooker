using FlightService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightService.Services
{
    public class FlightDataService
    {
        private readonly IMongoCollection<Flight> _flights;

        //  Konstruktor für Unit-Tests (Mock wird übergeben)
        public FlightDataService(IMongoCollection<Flight> collection)
        {
            _flights = collection;
        }

        //  Konstruktor für echten Betrieb mit AppSettings
        public FlightDataService(IOptions<FlightDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _flights = database.GetCollection<Flight>(settings.Value.FlightCollectionName);
        }

        //  Get: Alle Flüge (für Tests geeignet mit FindAsync)
        public async Task<List<Flight>> GetAsync()
        {
            var filter = Builders<Flight>.Filter.Empty;
            using var cursor = await _flights.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        //  Get: Einzelner Flug via ID
        public async Task<Flight?> GetAsync(string id)
        {
            var filter = Builders<Flight>.Filter.Eq(f => f.Id, id);
            using var cursor = await _flights.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }

        //  Neuen Flug erstellen
        public async Task CreateAsync(Flight newFlight)
        {
            await _flights.InsertOneAsync(newFlight);
        }
    }
}
