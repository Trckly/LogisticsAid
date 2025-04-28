using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class LogisticianRepository : ILogisticianRepository
{
    private readonly LogisticsAidDbContext _context;

    public LogisticianRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }

    public async Task<Logistician?> GetLogisticianAsync(Guid id, CancellationToken ct)
    {
        return await _context.Logisticians.FindAsync([id], cancellationToken: ct);
    }

    public async Task<Logistician?> GetLogisticianAsync(string email, CancellationToken ct)
    {
        return await _context.Logisticians.FirstOrDefaultAsync(l => l.ContactInfo.Email == email, cancellationToken: ct);
    }

    public async Task<IEnumerable<Logistician>> GetAllLogisticianAsync(CancellationToken ct)
    {
        return await _context.Logisticians.ToListAsync(ct);
    }

    public async Task UpdateLogisticianAsync(Logistician logistician, CancellationToken ct)
    {
        _context.Logisticians.Update(logistician);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateLogisticianAsync(Logistician logistician, CancellationToken ct)
    {
        await _context.Logisticians.AddAsync(logistician, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteLogisticianAsync(Guid id, CancellationToken ct)
    {
        var logistician = await _context.Logisticians.FindAsync([id], cancellationToken: ct);
        if (logistician != null)
        {
            _context.Logisticians.Remove(logistician);
            await _context.SaveChangesAsync(ct);
        }
    }
}