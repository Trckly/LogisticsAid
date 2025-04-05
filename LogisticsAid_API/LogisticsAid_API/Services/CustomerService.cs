using AutoMapper;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;

namespace LogisticsAid_API.Services;

public class CustomerService
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDTO?> GetCustomerByIdAsync(string customerId, CancellationToken ct)
    {
        var customer = await _customerRepository.GetCustomerAsync(Guid.Parse(customerId), ct);
        return _mapper.Map<CustomerDTO>(customer);
    }
    
    public async Task<CustomerDTO?> GetCustomerByPhoneAsync(string phone, CancellationToken ct)
    {
        var customer = await _customerRepository.GetCustomerAsync(phone, ct);
        return _mapper.Map<CustomerDTO>(customer);
    }

    public async Task AddCustomerAsync(CustomerDTO customerDTO, CancellationToken ct)
    {
        var customer = _mapper.Map<Customer>(customerDTO);
        await _customerRepository.UpdateCustomerAsync(customer, ct);
    }
}