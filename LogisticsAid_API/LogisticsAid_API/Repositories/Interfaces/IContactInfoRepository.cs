using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface IContactInfoRepository
{
    public Task<ContactInfo?> GetContactInfoAsync(Guid id, CancellationToken ct);
    public Task<ContactInfo?> GetContactInfoAsync(string email, CancellationToken ct);
    public Task<ContactInfo?> GetContactInfoAsync(ContactInfo contactInfo, CancellationToken ct);
    public Task<IEnumerable<ContactInfo>> GetAllContactInfoAsync(CancellationToken ct);
    public Task UpsertContactInfoAsync(ContactInfo contactInfo, CancellationToken ct);
    public Task UpdateContactInfoAsync(ContactInfo contactInfo, CancellationToken ct);
    public Task CreateContactInfoAsync(ContactInfo contactInfo, CancellationToken ct);
    public Task DeleteContactInfoAsync(Guid id, CancellationToken ct);
}