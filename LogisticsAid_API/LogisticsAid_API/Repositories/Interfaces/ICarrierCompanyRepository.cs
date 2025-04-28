using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface ICarrierCompanyRepository
{
    public Task<CarrierCompany?> GetCarrierAsync(string companyName, CancellationToken ct);
    public Task<CarrierCompany?> GetCarrierByCompanyNameAsync(string companyName, CancellationToken ct);
    public Task<IEnumerable<CarrierCompany>> GetAllCarriersAsync(CancellationToken ct);
    public Task UpsertCarrierAsync(CarrierCompany carrierCompany, CancellationToken ct);
    public Task UpdateCarrierAsync(CarrierCompany carrierCompany, CancellationToken ct);
    public Task CreateCarrierAsync(CarrierCompany carrierCompany, CancellationToken ct);
    public Task DeleteCarrierAsync(Guid id, CancellationToken ct);
}
