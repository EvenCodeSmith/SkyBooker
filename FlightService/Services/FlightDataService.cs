using FlightService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightService.Services;

public class FlightDataService
{
    private readonly IMongoCollection<Flight> _flights;

    public FlightDataService(IOptions<FlightDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _flights = database.GetCollection<Flight>(settings.Value.FlightCollectionName);
    }

    public async Task<List<Flight>> GetAsync() =>
        await _flights.Find(_ => true).ToListAsync();

    public async Task<Flight?> GetAsync(string id) =>
        await _flights.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Flight newFlight) =>
        await _flights.InsertOneAsync(newFlight);
}
