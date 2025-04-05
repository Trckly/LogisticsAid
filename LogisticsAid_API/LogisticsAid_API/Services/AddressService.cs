using AutoMapper;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;

namespace LogisticsAid_API.Services;

public class AddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressService(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<AddressDTO?> GetAddressAsync(string id, CancellationToken ct)
    {
        var address = await _addressRepository.GetAddressAsync(Guid.Parse(id), ct);
        return _mapper.Map<AddressDTO?>(address);
    }
    
    public async Task<AddressDTO> GetAddressAsync(AddressDTO addressDto, CancellationToken ct)
    {
        var address = _mapper.Map<Address>(addressDto);
        var addressResult = await _addressRepository.GetAddressAsync(address, ct);
        return _mapper.Map<AddressDTO>(addressResult);
    }

    public async Task UpsertAddressAsync(AddressDTO addressDto, CancellationToken ct)
    {
        var address = _mapper.Map<Address>(addressDto);
        await _addressRepository.UpsertAddressAsync(address, ct);
    }
}