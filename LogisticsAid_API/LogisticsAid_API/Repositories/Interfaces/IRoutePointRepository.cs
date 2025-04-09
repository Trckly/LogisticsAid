using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface IRoutePointRepository
{
    public Task<RoutePoint?> GetRoutePointAsync(Guid tripId, Guid addressId, CancellationToken ct);
    public Task<IEnumerable<RoutePoint>> GetRoutePointsByTripAsync(Guid tripId, CancellationToken ct);
    public Task<IEnumerable<RoutePoint>> GetRoutePointsByTypeAsync(ERoutePointType type, CancellationToken ct);
    public Task<IEnumerable<RoutePoint>> GetAllRoutePointsAsync(CancellationToken ct);
    public Task<IEnumerable<RoutePoint>> GetRoutePointsByIdAsync(IEnumerable<Guid> routePointIds, CancellationToken ct);
    public Task UpdateRoutePointAsync(RoutePoint routePoint, CancellationToken ct);
    public Task UpdateRoutePointRangeAsync(IEnumerable<RoutePoint> routePoint, CancellationToken ct);
    public Task CreateRoutePointAsync(RoutePoint routePoint, CancellationToken ct);
    public Task CreateRoutePointRangeAsync(IEnumerable<RoutePoint> routePoints, CancellationToken ct);
    public Task DeleteRoutePointAsync(Guid tripId, Guid addressId, CancellationToken ct);
}
