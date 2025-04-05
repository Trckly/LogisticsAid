using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface ICustomerRepository
{
    public Customer? GetCustomer(Guid id);
    public Customer? GetCustomer(string phone);
    public Task<Customer?> GetCustomerAsync(Guid id, CancellationToken ct);
    public Task<Customer?> GetCustomerAsync(string phone, CancellationToken ct);
    public Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken ct);
    public Task UpsertCustomerAsync(Customer customer, CancellationToken ct);
    public Task UpdateCustomerAsync(Customer customer, CancellationToken ct);
    public Task CreateCustomerAsync(Customer customer, CancellationToken ct);
    public Task DeleteCustomerAsync(Guid id, CancellationToken ct);
}