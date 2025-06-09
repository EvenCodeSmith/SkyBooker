using FlightService.Models;
using FlightService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FlightService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FlightController : ControllerBase
{
    private readonly FlightDataService _flightService;

    public FlightController(FlightDataService flightService)
    {
        _flightService = flightService;
    }

    [HttpGet]
    public async Task<List<Flight>> Get() =>
        await _flightService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Flight>> Get(string id)
    {
        try
        {
            var flight = await _flightService.GetAsync(id);
            if (flight is null) return NotFound();
            return Ok(flight);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }

    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post(Flight newFlight)
    {
        // Generate a new ObjectId for the flight
        newFlight.Id = ObjectId.GenerateNewId().ToString();
        await _flightService.CreateAsync(newFlight);
        return CreatedAtAction(nameof(Get), new { id = newFlight.Id }, newFlight);
    }
}
