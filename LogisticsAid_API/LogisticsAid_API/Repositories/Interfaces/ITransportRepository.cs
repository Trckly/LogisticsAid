using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface ITransportRepository
{
    public Task<Transport?> GetTransportAsync(string licencePlate, CancellationToken ct);
    public Task<IEnumerable<Transport>> GetAllTransportAsync(CancellationToken ct);
    public Task UpsertTransportAsync(Transport transport, CancellationToken ct);
    public Task UpdateTransportAsync(Transport transport, CancellationToken ct);
    public Task CreateTransportAsync(Transport transport, CancellationToken ct);
    public Task DeleteTransportAsync(string licencePlate, CancellationToken ct);
}
