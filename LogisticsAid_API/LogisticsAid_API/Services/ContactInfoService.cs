using AutoMapper;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;

namespace LogisticsAid_API.Services;

public class ContactInfoService
{
    private readonly IContactInfoRepository _contactInfoRepository;
    private readonly IMapper _mapper;

    public ContactInfoService(IContactInfoRepository contactInfoRepository, IMapper mapper)
    {
        _contactInfoRepository = contactInfoRepository;
        _mapper = mapper;
    }

    public async Task<ContactInfoDTO> GetContactInfoAsync(string id, CancellationToken ct)
    {
        var contactInfo = await _contactInfoRepository.GetContactInfoAsync(Guid.Parse(id), ct);
        return _mapper.Map<ContactInfoDTO>(contactInfo);
    }
    public async Task<ContactInfoDTO> GetContactInfoAsync(ContactInfoDTO contactInfoDto, CancellationToken ct)
    {
        var contactInfo = _mapper.Map<ContactInfo>(contactInfoDto);
        var contactInfoDtoResult = await _contactInfoRepository.GetContactInfoAsync(contactInfo, ct);
        return _mapper.Map<ContactInfoDTO>(contactInfoDtoResult);
    }
    public async Task UpsertContactInfoAsync(ContactInfoDTO contactInfoDto, CancellationToken ct)
    {
        var contactInfo = _mapper.Map<ContactInfo>(contactInfoDto);
        await _contactInfoRepository.UpsertContactInfoAsync(contactInfo, ct);
    }
}