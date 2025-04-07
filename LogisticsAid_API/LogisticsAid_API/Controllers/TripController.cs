using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsAid_API.Controllers;

[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class TripController : BaseController
{
    private readonly TripService _tripService;

    public TripController(TripService tripService)
    {
        _tripService = tripService;
    }
    
    [HttpGet]
    public Task<ActionResult> GetAllTrips(CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var tripsDto = await _tripService.GetAllTripsAsync(ct);

            return Ok(tripsDto);
        });

    [HttpGet]
    public Task<ActionResult> GetTrips(
        [FromQuery] int page, 
        [FromQuery] int pageSize, 
        CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var totalItems = await _tripService.CountAsync(ct);
            var items = await _tripService.GetTripsAsync(page, pageSize, ct);

            var response = new PaginatedResponse<TripDTO>
            {
                Items = items,
                TotalCount = totalItems,
                PageIndex = page,
                PageSize = pageSize
            };
            
            return Ok(response);
        });
    
    [HttpGet("{id}")]
    public Task<ActionResult> GetTripById(string id, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var trip = await _tripService.GetTripByIdAsync(id, ct);

            return Ok(trip);
        });
    
    [HttpPost]
    public Task<ActionResult> AddTrip([FromBody] TripDTO tripDto, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            await _tripService.AddTripAsync(tripDto, ct);
            return Ok();
        });
}