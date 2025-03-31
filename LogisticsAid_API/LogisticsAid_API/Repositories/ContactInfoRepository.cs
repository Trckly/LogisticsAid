using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class ContactInfoRepository : IContactInfoRepository
{
    private readonly LogisticsAidDbContext _context;
    
    public ContactInfoRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }
    
    public async Task<ContactInfo?> GetContactInfoAsync(Guid id, CancellationToken ct)
    {
        return await _context.ContactInfo.FindAsync([id], cancellationToken: ct);
    }

    public async Task<ContactInfo?> GetContactInfoAsync(string email, CancellationToken ct)
    {
        return await _context.ContactInfo.FirstOrDefaultAsync(x => x.Email == email, cancellationToken: ct);
    }

    public async Task<IEnumerable<ContactInfo>> GetAllContactInfoAsync(CancellationToken ct)
    {
        return await _context.ContactInfo.ToListAsync(ct);
    }

    public async Task UpdateContactInfoAsync(ContactInfo contactInfo, CancellationToken ct)
    {
        _context.ContactInfo.Update(contactInfo);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateContactInfoAsync(ContactInfo contactInfo, CancellationToken ct)
    {
        await _context.ContactInfo.AddAsync(contactInfo, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteContactInfoAsync(Guid id, CancellationToken ct)
    {
        var contactInfo = await _context.ContactInfo.FindAsync([id], cancellationToken: ct);
        if (contactInfo != null)
        {
            _context.ContactInfo.Remove(contactInfo);
            await _context.SaveChangesAsync(ct);
        }
    }
}