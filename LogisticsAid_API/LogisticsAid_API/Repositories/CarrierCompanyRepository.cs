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

    public async Task<CarrierCompany?> GetCarrierAsync(string companyName, CancellationToken ct)
    {
        return await _context.CarrierCompanies
            .FindAsync([companyName], ct);
    }

    public async Task<CarrierCompany?> GetCarrierByCompanyNameAsync(string companyName, CancellationToken ct)
    {
        return await _context.CarrierCompanies
            .FirstOrDefaultAsync(c => c.CompanyName == companyName, ct);
    }

    public async Task<IEnumerable<CarrierCompany>> GetAllCarriersAsync(CancellationToken ct)
    {
        return await _context.CarrierCompanies.ToListAsync(ct);
    }

    public async Task UpsertCarrierAsync(CarrierCompany carrierCompany, CancellationToken ct)
    {
        var existingCarrier = await _context.CarrierCompanies.FindAsync([carrierCompany.CompanyName], ct);
        if (existingCarrier == null)
        {
            await _context.CarrierCompanies.AddAsync(carrierCompany, ct);
        }
        else
        {
            _context.CarrierCompanies.Update(carrierCompany);
        }

        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateCarrierAsync(CarrierCompany carrierCompany, CancellationToken ct)
    {
        _context.CarrierCompanies.Update(carrierCompany);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateCarrierAsync(CarrierCompany carrierCompany, CancellationToken ct)
    {
        await _context.CarrierCompanies.AddAsync(carrierCompany, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteCarrierAsync(Guid id, CancellationToken ct)
    {
        var carrier = await _context.CarrierCompanies.FindAsync([id], cancellationToken: ct);
        if (carrier != null)
        {
            _context.CarrierCompanies.Remove(carrier);
            await _context.SaveChangesAsync(ct);
        }
    }
}