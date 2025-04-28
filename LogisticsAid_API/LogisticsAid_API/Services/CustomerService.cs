using AutoMapper;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;

namespace LogisticsAid_API.Services;

public class CustomerService
{
    private readonly IMapper _mapper;
    private readonly ICustomerCompanyRepository _customerCompanyRepository;

    public CustomerService(IMapper mapper, ICustomerCompanyRepository customerCompanyRepository)
    {
        _mapper = mapper;
        _customerCompanyRepository = customerCompanyRepository;
    }

    public async Task<CustomerCompanyDTO?> GetCustomerCompanyByIdAsync(string customerCompanyId, CancellationToken ct)
    {
        var customer = await _customerCompanyRepository.GetCustomerCompanyAsync(customerCompanyId, ct);
        return _mapper.Map<CustomerCompanyDTO>(customer);
    }
    
    public async Task<CustomerCompanyDTO?> GetCustomerCompanyByPhoneAsync(string phone, CancellationToken ct)
    {
        var customer = await _customerCompanyRepository.GetCustomerCompanyAsync(phone, ct);
        return _mapper.Map<CustomerCompanyDTO>(customer);
    }

    public async Task AddCustomerCompanyAsync(CustomerCompanyDTO customerCompanyDto, CancellationToken ct)
    {
        var customer = _mapper.Map<CustomerCompany>(customerCompanyDto);
        await _customerCompanyRepository.UpdateCustomerCompanyAsync(customer, ct);
    }
}