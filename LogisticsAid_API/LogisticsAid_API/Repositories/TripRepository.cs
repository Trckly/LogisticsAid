using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class TripRepository : ITripRepository
{
    private readonly LogisticsAidDbContext _context;

    public TripRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }

    public async Task<Trip?> GetTripAsync(Guid id, CancellationToken ct)
    {
        return await _context.Trips
            .Include(t => t.RoutePoints)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<Trip?> GetTripAsync(string readableId, CancellationToken ct)
    {
        return await _context.Trips.SingleOrDefaultAsync(t => t.ReadableId == readableId, ct);
    }

    public async Task<IEnumerable<Trip>> GetTripsAsync(int page, int pageSize, CancellationToken ct)
    {
        return await _context.Trips
            .OrderBy(t => t.ReadableId)
            .Skip(page * pageSize)
            .Take(pageSize)
            .Include(t => t.Logistician)
            .ThenInclude(logistician => logistician.Contact)
            .Include(t => t.Driver)
            .ThenInclude(driver => driver.Contact)
            .Include(t => t.Customer)
            .ThenInclude(customer => customer.Contact)
            .Include(t => t.Carrier)
            .ThenInclude(carrier => carrier.Contact)
            .Include(t => t.Transport)
            .ToListAsync(ct);
    }


    public async Task<Trip?> GetTripByReadableIdAsync(string readableId, CancellationToken ct)
    {
        return await _context.Trips
            .Include(t => t.RoutePoints)
            .FirstOrDefaultAsync(t => t.ReadableId == readableId, ct);
    }

    public async Task<IEnumerable<Trip>> GetTripsByLogisticianAsync(Guid logisticianId, CancellationToken ct)
    {
        return await _context.Trips
            .Where(t => t.LogisticianId == logisticianId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Trip>> GetTripsByCustomerAsync(Guid customerId, CancellationToken ct)
    {
        return await _context.Trips
            .Where(t => t.CustomerId == customerId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Trip>> GetTripsByCarrierAsync(Guid carrierId, CancellationToken ct)
    {
        return await _context.Trips
            .Where(t => t.CarrierId == carrierId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Trip>> GetTripsByDriverAsync(Guid driverId, CancellationToken ct)
    {
        return await _context.Trips
            .Where(t => t.DriverId == driverId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Trip>> GetTripsByDateRangeAsync(DateTime start, DateTime end, CancellationToken ct)
    {
        return await _context.Trips
            .Where(t => t.LoadingDate >= start && t.UnloadingDate <= end)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Trip>> GetAllTripsAsync(CancellationToken ct)
    {
        return await _context.Trips
            .Include(t => t.Logistician)
            .ThenInclude(logistician => logistician.Contact)
            .Include(t => t.Driver)
            .ThenInclude(driver => driver.Contact)
            .Include(t => t.Customer)
            .ThenInclude(customer => customer.Contact)
            .Include(t => t.Carrier)
            .ThenInclude(carrier => carrier.Contact)
            .Include(t => t.Transport)
            .ToListAsync(ct);
    }

    public async Task AddTripAsync(Trip trip, CancellationToken ct)
    {
        await _context.Trips.AddAsync(trip, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateTripAsync(Trip trip, CancellationToken ct)
    {
        _context.Trips.Update(trip);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateTripAsync(Trip trip, CancellationToken ct)
    {
        await _context.Trips.AddAsync(trip, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteTripAsync(Guid id, CancellationToken ct)
    {
        var trip = await _context.Trips.FindAsync([id], cancellationToken: ct);
        if (trip != null)
        {
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<int> CountAsync(CancellationToken ct)
    {
        return await _context.Trips.CountAsync(ct);
    }
}
