using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class CarrierRepository : ICarrierRepository
{
    private readonly LogisticsAidDbContext _context;

    public CarrierRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }

    public async Task<Carrier?> GetCarrierAsync(Guid id, CancellationToken ct)
    {
        return await _context.Carriers.FindAsync([id], cancellationToken: ct);
    }

    public async Task<Carrier?> GetCarrierAsync(string phone, CancellationToken ct)
    {
        return await _context.Carriers.SingleOrDefaultAsync(c => c.Contact.Phone == phone, cancellationToken: ct);
    }

    public async Task<Carrier?> GetCarrierByCompanyNameAsync(string companyName, CancellationToken ct)
    {
        return await _context.Carriers.FirstOrDefaultAsync(c => c.CompanyName == companyName, ct);
    }

    public async Task<IEnumerable<Carrier>> GetAllCarriersAsync(CancellationToken ct)
    {
        return await _context.Carriers.ToListAsync(ct);
    }

    public async Task UpsertCarrierAsync(Carrier carrier, CancellationToken ct)
    {
        var existingCarrier = await _context.Carriers.FindAsync([carrier.ContactId], ct);
        if (existingCarrier == null)
        {
            await _context.Carriers.AddAsync(carrier, ct);
        }
        else
        {
            _context.Carriers.Update(carrier);
        }

        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateCarrierAsync(Carrier carrier, CancellationToken ct)
    {
        _context.Carriers.Update(carrier);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateCarrierAsync(Carrier carrier, CancellationToken ct)
    {
        await _context.Carriers.AddAsync(carrier, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteCarrierAsync(Guid id, CancellationToken ct)
    {
        var carrier = await _context.Carriers.FindAsync([id], cancellationToken: ct);
        if (carrier != null)
        {
            _context.Carriers.Remove(carrier);
            await _context.SaveChangesAsync(ct);
        }
    }
}