using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly LogisticsAidDbContext _context;

    public AddressRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }

    public async Task<Address?> GetAddressAsync(Guid id, CancellationToken ct)
    {
        return await _context.Addresses.FindAsync([id], cancellationToken: ct);
    }

    public async Task<Address?> GetAddressAsync(Address address, CancellationToken ct)
    {
        return await _context.Addresses.SingleOrDefaultAsync(a =>
                a.Country == address.Country &&
                a.Province == address.Province &&
                a.City == address.City &&
                a.Street == address.Street &&
                a.Number == address.Number,
            ct);
    }

    public async Task<IEnumerable<Address>> GetAllAddressesAsync(CancellationToken ct)
    {
        return await _context.Addresses.ToListAsync(ct);
    }

    public async Task<IEnumerable<Address>> GetAddressesByCity(string city, CancellationToken ct)
    {
        return await _context.Addresses.Where(a => a.City == city).ToListAsync(ct);
    }

    public async Task UpsertAddressAsync(Address address, CancellationToken ct)
    {
        var existingAddress = await GetAddressAsync(address, ct);
        if (existingAddress == null)
        {
            await _context.Addresses.AddAsync(address, ct);
        }
        else
        {
            _context.Addresses.Update(address);
        }

        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAddressAsync(Address address, CancellationToken ct)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateAddressAsync(Address address, CancellationToken ct)
    {
        await _context.Addresses.AddAsync(address, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAddressAsync(Guid id, CancellationToken ct)
    {
        var address = await _context.Addresses.FindAsync([id], cancellationToken: ct);
        if (address != null)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync(ct);
        }
    }
}