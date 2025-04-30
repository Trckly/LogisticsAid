using AutoMapper;
using LogisticsAid_API.Context;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Auxiliary;
using LogisticsAid_API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Services;

public class RoutePointTripService
{
    private readonly LogisticsAidDbContext _context;
    private readonly IMapper _mapper;
    
    public RoutePointTripService(LogisticsAidDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task BindRoutePointToTripAsync(RoutePointDTO existingRoutePointDto, TripDTO tripDto, CancellationToken ct)
    {
        var routePointTrip = new RoutePointTrip
        {
            RoutePointId = Guid.Parse(existingRoutePointDto.Id), TripId = Guid.Parse(tripDto.Id)
        };
        
        var existingRoutePointTrip = await _context.RoutePointTrip
            .FindAsync([routePointTrip.RoutePointId, routePointTrip.TripId], ct);
        
        if (existingRoutePointTrip == null)
        {
            await _context.RoutePointTrip.AddAsync(routePointTrip, ct);
        }
        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<RoutePointDTO>> GetRoutePointsByTripIdsAsync(ICollection<string> tripIds, CancellationToken ct)
    {
        var routePointDtos = await _context.RoutePointTrip
            .Where(rpt => tripIds.Contains(rpt.TripId.ToString()))
            .Include(rp => rp.RoutePoint.Address)
            .Select(rpt => _mapper.Map<RoutePointDTO>(rpt.RoutePoint)).ToListAsync(ct);
        
        return routePointDtos;
    }

    public async Task<List<RoutePointTripDTO>> GetRoutePointsTripsAsync(ICollection<string> tripIds, CancellationToken ct)
    {
        var routePointTripsDto = await _context.RoutePointTrip
            .Where(rpt => tripIds.Contains(rpt.TripId.ToString()))
            .Select(rpt => _mapper.Map<RoutePointTripDTO>(rpt))
            .ToListAsync(ct);
        
        return routePointTripsDto;
    }
}