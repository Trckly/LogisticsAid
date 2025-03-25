using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface ILogisticianRepository
{
    public Task<Logistician?> GetLogisticianAsync(Guid id, CancellationToken ct);
    public Task<Logistician?> GetLogisticianAsync(string email, CancellationToken ct);
    public Task<IEnumerable<Logistician>> GetAllLogisticianAsync(CancellationToken ct);
    public Task UpdateLogisticianAsync(Logistician logistician, CancellationToken ct);
    public Task CreateLogisticianAsync(Logistician log, CancellationToken ct);
    public Task DeleteLogisticianAsync(Guid id, CancellationToken ct);
}