using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface ICarrierRepository
{
    public Task<Carrier?> GetCarrierAsync(Guid id, CancellationToken ct);
    public Task<Carrier?> GetCarrierAsync(string phone, CancellationToken ct);
    public Task<Carrier?> GetCarrierByCompanyNameAsync(string companyName, CancellationToken ct);
    public Task<IEnumerable<Carrier>> GetAllCarriersAsync(CancellationToken ct);
    public Task UpsertCarrierAsync(Carrier carrier, CancellationToken ct);
    public Task UpdateCarrierAsync(Carrier carrier, CancellationToken ct);
    public Task CreateCarrierAsync(Carrier carrier, CancellationToken ct);
    public Task DeleteCarrierAsync(Guid id, CancellationToken ct);
}
