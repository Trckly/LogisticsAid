using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Enums;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class RoutePointRepository : IRoutePointRepository
{
    private readonly LogisticsAidDbContext _context;

    public RoutePointRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }

    public async Task<RoutePoint?> GetRoutePointAsync(Guid tripId, Guid addressId, CancellationToken ct)
    {
        return await _context.RoutePoints
            .FirstOrDefaultAsync(rp => rp.TripId == tripId && rp.AddressId == addressId, ct);
    }

    public async Task<IEnumerable<RoutePoint>> GetRoutePointsByTripAsync(Guid tripId, CancellationToken ct)
    {
        return await _context.RoutePoints
            .Where(rp => rp.TripId == tripId)
            .OrderBy(rp => rp.Sequence)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<RoutePoint>> GetRoutePointsByTypeAsync(ERoutePointType type, CancellationToken ct)
    {
        return await _context.RoutePoints
            .Where(rp => rp.Type == type)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<RoutePoint>> GetAllRoutePointsAsync(CancellationToken ct)
    {
        return await _context.RoutePoints.ToListAsync(ct);
    }

    public async Task UpdateRoutePointAsync(RoutePoint routePoint, CancellationToken ct)
    {
        _context.RoutePoints.Update(routePoint);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateRoutePointRangeAsync(IEnumerable<RoutePoint> routePoint, CancellationToken ct)
    {
        _context.RoutePoints.UpdateRange(routePoint);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateRoutePointAsync(RoutePoint routePoint, CancellationToken ct)
    {
        await _context.RoutePoints.AddAsync(routePoint, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateRoutePointRangeAsync(IEnumerable<RoutePoint> routePoints, CancellationToken ct)
    {
        await _context.RoutePoints.AddRangeAsync(routePoints, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteRoutePointAsync(Guid tripId, Guid addressId, CancellationToken ct)
    {
        var routePoint = await _context.RoutePoints
            .FirstOrDefaultAsync(rp => rp.TripId == tripId && rp.AddressId == addressId, ct);

        if (routePoint != null)
        {
            _context.RoutePoints.Remove(routePoint);
            await _context.SaveChangesAsync(ct);
        }
    }
}