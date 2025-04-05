using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface IAddressRepository
{
    public Task<Address?> GetAddressAsync(Guid id, CancellationToken ct);
    public Task<Address?> GetAddressAsync(Address address, CancellationToken ct);
    public Task<IEnumerable<Address>> GetAllAddressesAsync(CancellationToken ct);
    public Task<IEnumerable<Address>> GetAddressesByCity(string city, CancellationToken ct);
    public Task UpsertAddressAsync(Address address, CancellationToken ct);
    public Task UpdateAddressAsync(Address address, CancellationToken ct);
    public Task CreateAddressAsync(Address address, CancellationToken ct);
    public Task DeleteAddressAsync(Guid id, CancellationToken ct);
}
