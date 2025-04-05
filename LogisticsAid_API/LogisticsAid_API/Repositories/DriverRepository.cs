using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly LogisticsAidDbContext _context;

    public DriverRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }

    public async Task<Driver?> GetDriverAsync(Guid id, CancellationToken ct)
    {
        return await _context.Drivers.FindAsync([id], cancellationToken: ct);
    }

    public async Task<Driver?> GetDriverAsync(string phone, CancellationToken ct)
    {
        return await _context.Drivers.SingleOrDefaultAsync(d => d.Contact.Phone == phone, cancellationToken: ct);
    }

    public async Task<Driver?> GetDriverByLicenseAsync(string license, CancellationToken ct)
    {
        return await _context.Drivers.FirstOrDefaultAsync(d => d.License == license, ct);
    }

    public async Task<IEnumerable<Driver>> GetDriversByCompanyNameAsync(string companyName, CancellationToken ct)
    {
        return await _context.Drivers.Where(d => d.CompanyName == companyName).ToListAsync(ct);
    }

    public async Task<IEnumerable<Driver>> GetAllDriversAsync(CancellationToken ct)
    {
        return await _context.Drivers.ToListAsync(ct);
    }

    public async Task UpsertDriverAsync(Driver driver, CancellationToken ct)
    {
        var existingDriver = await _context.Drivers.FindAsync([driver.ContactId], ct);
        if (existingDriver != null)
        {
            _context.Drivers.Update(driver);
        }
        else
        {
            await _context.Drivers.AddAsync(driver, ct);
        }
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateDriverAsync(Driver driver, CancellationToken ct)
    {
        _context.Drivers.Update(driver);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateDriverAsync(Driver driver, CancellationToken ct)
    {
        await _context.Drivers.AddAsync(driver, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteDriverAsync(Guid id, CancellationToken ct)
    {
        var driver = await _context.Drivers.FindAsync([id], cancellationToken: ct);
        if (driver != null)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync(ct);
        }
    }
}