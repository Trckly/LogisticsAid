using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly LogisticsAidDbContext _context;

    public CustomerRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }

    public Customer? GetCustomer(Guid id)
    {
        return _context.Customers.Find([id]);
    }

    public Customer? GetCustomer(string phone)
    {
        return _context.Customers.SingleOrDefault(c => c.Contact.Phone == phone);
    }

    public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken ct)
    {
        return await _context.Customers.FindAsync([id], cancellationToken: ct);
    }

    public async Task<Customer?> GetCustomerAsync(string phone, CancellationToken ct)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Contact.Phone == phone, ct);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken ct)
    {
        return await _context.Customers.ToListAsync(ct);
    }

    public async Task UpsertCustomerAsync(Customer customer, CancellationToken ct)
    {
        var existingCustomer = await _context.Customers.FindAsync([customer.ContactId], ct);

        if (existingCustomer != null)
        {
            _context.Customers.Update(customer);
        }
        else
        {
            await _context.Customers.AddAsync(customer, ct);
        }

        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateCustomerAsync(Customer customer, CancellationToken ct)
    {

        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateCustomerAsync(Customer customer, CancellationToken ct)
    {
        await _context.Customers.AddAsync(customer, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken ct)
    {
        var customer = await _context.Customers.FindAsync([id], cancellationToken: ct);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(ct);
        }
    }
}