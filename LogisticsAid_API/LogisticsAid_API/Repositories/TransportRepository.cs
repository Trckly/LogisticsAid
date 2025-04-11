using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class TransportRepository : ITransportRepository
{
    private readonly LogisticsAidDbContext _context;

    public TransportRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }

    public async Task<Transport?> GetTransportAsync(string licencePlate, CancellationToken ct)
    {
        return await _context.Transport.FindAsync([licencePlate], cancellationToken: ct);
    }

    public async Task<IEnumerable<Transport>> GetTransportByCompanyAsync(string companyName, CancellationToken ct)
    {
        return await _context.Transport
            .Where(t => t.CompanyName == companyName)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Transport>> GetAllTransportAsync(CancellationToken ct)
    {
        return await _context.Transport.ToListAsync(ct);
    }

    public async Task UpsertTransportAsync(Transport transport, CancellationToken ct)
    {
        var existingTransport = await _context.Transport.FindAsync([transport.LicensePlate], ct);
        if (existingTransport == null)
        {
            await _context.Transport.AddAsync(transport, ct);
        }
        else
        {
            _context.Entry(existingTransport).CurrentValues.SetValues(transport);
        }

        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateTransportAsync(Transport transport, CancellationToken ct)
    {
        _context.Transport.Update(transport);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateTransportAsync(Transport transport, CancellationToken ct)
    {
        await _context.Transport.AddAsync(transport, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteTransportAsync(string licencePlate, CancellationToken ct)
    {
        var transport = await _context.Transport.FindAsync([licencePlate], cancellationToken: ct);
        if (transport != null)
        {
            _context.Transport.Remove(transport);
            await _context.SaveChangesAsync(ct);
        }
    }
}
