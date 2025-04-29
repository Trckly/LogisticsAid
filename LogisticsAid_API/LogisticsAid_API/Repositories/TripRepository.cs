using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Exceptions;
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
            .ThenInclude(logistician => logistician.ContactInfo)
            .Include(t => t.Driver)
            .ThenInclude(driver => driver.ContactInfo)
            .Include(t => t.CustomerCompany)
            .Include(t => t.CarrierCompany)
            .Include(t => t.Truck)
            .Include(t => t.Trailer)
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

    public async Task<IEnumerable<Trip>> GetTripsByCustomerCompanyAsync(string customerCompanyId, CancellationToken ct)
    {
        return await _context.Trips
            .Where(t => t.CustomerCompanyId == customerCompanyId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Trip>> GetTripsByCarrierCompanyAsync(string carrierCompanyId, CancellationToken ct)
    {
        return await _context.Trips
            .Where(t => t.CarrierCompanyId == carrierCompanyId)
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
            .ThenInclude(logistician => logistician.ContactInfo)
            .Include(t => t.Driver)
            .ThenInclude(driver => driver.ContactInfo)
            .Include(t => t.CustomerCompany)
            .ThenInclude(customer => customer.Contacts)
            .Include(t => t.CarrierCompany)
            .ThenInclude(carrier => carrier.Contacts)
            .Include(t => t.Truck)
            .Include(t => t.Trailer)
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

    public async Task DeleteTripsAsync(IEnumerable<Guid> tripIds, CancellationToken ct)
    {
        var tripsToDelete = new List<Trip>();
        foreach (var tripId in tripIds)
        {
            var trip = await _context.Trips.FindAsync([tripId], cancellationToken: ct);
            if (trip == null)
            {
                throw new EntryDoesntExistException();
            }
            
            tripsToDelete.Add(trip);
        }

        if (tripsToDelete.Count > 0)
        {
            _context.Trips.RemoveRange(tripsToDelete);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<int> CountAsync(CancellationToken ct)
    {
        return await _context.Trips.CountAsync(ct);
    }
}
