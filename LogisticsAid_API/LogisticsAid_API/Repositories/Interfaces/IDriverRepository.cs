using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface IDriverRepository
{
    public Task<Driver?> GetDriverAsync(Guid id, CancellationToken ct);
    public Task<Driver?> GetDriverAsync(string phone, CancellationToken ct);
    public Task<Driver?> GetDriverByLicenseAsync(string license, CancellationToken ct);
    public Task<IEnumerable<Driver>> GetAllDriversAsync(CancellationToken ct);
    public Task UpsertDriverAsync(Driver driver, CancellationToken ct);
    public Task UpdateDriverAsync(Driver driver, CancellationToken ct);
    public Task CreateDriverAsync(Driver driver, CancellationToken ct);
    public Task DeleteDriverAsync(Guid id, CancellationToken ct);
}
