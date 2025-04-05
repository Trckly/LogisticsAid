using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using LogisticsAid_API.DTOs;

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
        return await _context.ContactInfo.FirstOrDefaultAsync(ci => ci.Email == email, cancellationToken: ct);
    }

    public async Task<ContactInfo?> GetContactInfoAsync(ContactInfo contactInfo, CancellationToken ct)
    {
        return await _context.ContactInfo
            .SingleOrDefaultAsync(ci => ci.FirstName == contactInfo.FirstName 
                                        && ci.LastName == contactInfo.LastName
                                        && ci.Phone == contactInfo.Phone,
                cancellationToken: ct);
    }

    public async Task<IEnumerable<ContactInfo>> GetAllContactInfoAsync(CancellationToken ct)
    {
        return await _context.ContactInfo.ToListAsync(ct);
    }

    public async Task UpsertContactInfoAsync(ContactInfo contactInfo, CancellationToken ct)
    {
        var existingContactInfo = await _context.ContactInfo
            .SingleOrDefaultAsync(ci => ci.Phone == contactInfo.Phone, cancellationToken: ct);
        if (existingContactInfo == null)
        {
            await _context.ContactInfo.AddAsync(contactInfo, ct);
        }
        else
        {
            existingContactInfo.FirstName = contactInfo.FirstName;
            existingContactInfo.LastName = contactInfo.LastName;
            existingContactInfo.Phone = contactInfo.Phone;
            existingContactInfo.Email = contactInfo.Email;
            existingContactInfo.BirthDate = contactInfo.BirthDate;
            // _context.Entry(existingContactInfo).CurrentValues.SetValues(values);
        }
        await _context.SaveChangesAsync(ct);
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