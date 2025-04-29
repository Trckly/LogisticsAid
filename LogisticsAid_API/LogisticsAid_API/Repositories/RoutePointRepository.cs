using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Enums;
using LogisticsAid_API.Exceptions;
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
            .Where(rp => rp.RoutePointTrips.Any(rpt => rpt.TripId == tripId) && rp.AddressId == addressId)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<RoutePoint?> GetRoutePointAsync(RoutePoint routePoint, CancellationToken ct)
    {
        return await _context.RoutePoints.FirstOrDefaultAsync(rp => rp.Type == routePoint.Type &&
                                                                    rp.AddressId == routePoint.AddressId && 
                                                                    rp.Sequence == routePoint.Sequence && 
                                                                    rp.CompanyName == routePoint.CompanyName && 
                                                                    rp.AdditionalInfo == routePoint.AdditionalInfo, ct);
    }

    public IEnumerable<RoutePoint> GetRoutePointsByTrip(Guid tripId, CancellationToken ct)
    {
        return _context.RoutePoints
            .Where(rp => rp.RoutePointTrips.Any(rpt => rpt.TripId == tripId));
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

    public async Task<IEnumerable<RoutePoint>> GetRoutePointsByIdAsync(IEnumerable<Guid> routePointIds, CancellationToken ct)
    {
        return await _context.RoutePoints
            .Where(rp => routePointIds.Contains(rp.Id))
            .Include(rp => rp.Address)
            .ToListAsync(ct);
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
            .Where(rp => rp.RoutePointTrips.Any(rpt => rpt.TripId == tripId) && rp.AddressId == addressId)
            .FirstOrDefaultAsync(ct);

        if (routePoint != null)
        {
            _context.RoutePoints.Remove(routePoint);
            await _context.SaveChangesAsync(ct);
        }
        else
        {
            throw new EntryDoesntExistException();
        }
    }
}