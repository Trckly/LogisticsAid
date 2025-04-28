using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface ICustomerCompanyRepository
{
    public CustomerCompany? GetCustomerCompany(string companyName);
    public CustomerCompany? GetCustomerCompanyByPhone(string phone);
    public Task<CustomerCompany?> GetCustomerCompanyAsync(string companyName, CancellationToken ct);
    public Task<CustomerCompany?> GetCustomerCompanyByPhoneAsync(string phone, CancellationToken ct);
    public Task<IEnumerable<CustomerCompany>> GetAllCustomerCompaniesAsync(CancellationToken ct);
    public Task UpsertCustomerCompanyAsync(CustomerCompany customerCompany, CancellationToken ct);
    public Task UpdateCustomerCompanyAsync(CustomerCompany customerCompany, CancellationToken ct);
    public Task CreateCustomerCompanyAsync(CustomerCompany customerCompany, CancellationToken ct);
    public Task DeleteCustomerCompanyAsync(Guid id, CancellationToken ct);
}