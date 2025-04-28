using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface ITripRepository
{
    public Task<Trip?> GetTripAsync(Guid id, CancellationToken ct);
    public Task<Trip?> GetTripAsync(string readableId, CancellationToken ct);
    public Task<IEnumerable<Trip>> GetTripsAsync(int page, int pageSize, CancellationToken ct);
    public Task<Trip?> GetTripByReadableIdAsync(string readableId, CancellationToken ct);
    public Task<IEnumerable<Trip>> GetTripsByLogisticianAsync(Guid logisticianId, CancellationToken ct);
    public Task<IEnumerable<Trip>> GetTripsByCustomerCompanyAsync(string customerCompanyId, CancellationToken ct);
    public Task<IEnumerable<Trip>> GetTripsByCarrierCompanyAsync(string carrierCompanyId, CancellationToken ct);
    public Task<IEnumerable<Trip>> GetTripsByDriverAsync(Guid driverId, CancellationToken ct);
    public Task<IEnumerable<Trip>> GetTripsByDateRangeAsync(DateTime start, DateTime end, CancellationToken ct);
    public Task<IEnumerable<Trip>> GetAllTripsAsync(CancellationToken ct);
    public Task AddTripAsync(Trip trip, CancellationToken ct);
    public Task UpdateTripAsync(Trip trip, CancellationToken ct);
    public Task CreateTripAsync(Trip trip, CancellationToken ct);
    public Task DeleteTripsAsync(IEnumerable<Guid> tripIds, CancellationToken ct);
    public Task<int> CountAsync(CancellationToken ct);
}
