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

    public CustomerCompany? GetCustomerCompany(string companyName)
    {
        return _context.CustomerCompanies.Find([companyName]);
    }

    public CustomerCompany? GetCustomerCompanyByPhone(string phone)
    {
        return _context.CustomerCompanies
            .FirstOrDefault(cc => cc.Contacts.Any(c=>c.Phone == phone));
    }

    public async Task<CustomerCompany?> GetCustomerCompanyAsync(string companyName, CancellationToken ct)
    {
        return await _context.CustomerCompanies.FindAsync([companyName], cancellationToken: ct);
    }

    public async Task<CustomerCompany?> GetCustomerCompanyByPhoneAsync(string phone, CancellationToken ct)
    {
        return await _context.CustomerCompanies
            .FirstOrDefaultAsync(cc => cc.Contacts.Any(c => c.Phone == phone), ct);
    }

    public async Task<IEnumerable<CustomerCompany>> GetAllCustomerCompaniesAsync(CancellationToken ct)
    {
        return await _context.CustomerCompanies.ToListAsync(ct);
    }

    public async Task UpsertCustomerCompanyAsync(CustomerCompany customerCompany, CancellationToken ct)
    {
        var existingCustomer = await _context.CustomerCompanies.FindAsync([customerCompany.CompanyName], ct);

        if (existingCustomer != null)
        {
            _context.CustomerCompanies.Update(customerCompany);
        }
        else
        {
            await _context.CustomerCompanies.AddAsync(customerCompany, ct);
        }

        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateCustomerCompanyAsync(CustomerCompany customerCompany, CancellationToken ct)
    {

        _context.CustomerCompanies.Update(customerCompany);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CreateCustomerCompanyAsync(CustomerCompany customerCompany, CancellationToken ct)
    {
        await _context.CustomerCompanies.AddAsync(customerCompany, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteCustomerCompanyAsync(Guid id, CancellationToken ct)
    {
        var customer = await _context.CustomerCompanies.FindAsync([id], cancellationToken: ct);
        if (customer != null)
        {
            _context.CustomerCompanies.Remove(customer);
            await _context.SaveChangesAsync(ct);
        }
    }
}