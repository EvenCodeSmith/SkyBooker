using FlightService.Models;
using FlightService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightService.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var flight = await _flightService.GetAsync(id);
        if (flight is null) return NotFound();
        return flight;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Flight newFlight)
    {
        await _flightService.CreateAsync(newFlight);
        return CreatedAtAction(nameof(Get), new { id = newFlight.Id }, newFlight);
    }
}
