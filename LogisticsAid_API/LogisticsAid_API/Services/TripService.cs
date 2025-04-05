

using AutoMapper;
using LogisticsAid_API.Context;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;

namespace LogisticsAid_API.Services;

public class TripService
{
    private readonly LogisticsAidDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITripRepository _tripRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICarrierRepository _carrierRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly ITransportRepository _transportRepository;
    private readonly RoutePointService _routePointService;
    private readonly ContactInfoService _contactInfoService;
    private readonly AddressService _addressService;

    public TripService(LogisticsAidDbContext context, IMapper mapper, ITripRepository tripRepository, 
        ICustomerRepository customerRepository, ICarrierRepository carrierRepository, IDriverRepository driverRepository, 
        ITransportRepository transportRepository, RoutePointService routePointService, 
        ContactInfoService contactInfoService, AddressService addressService)
    {
        _context = context;
        _mapper = mapper;
        _tripRepository = tripRepository;
        _customerRepository = customerRepository;
        _carrierRepository = carrierRepository;
        _driverRepository = driverRepository;
        _transportRepository = transportRepository;
        _routePointService = routePointService;
        _contactInfoService = contactInfoService;
        _addressService = addressService;
    }

    public async Task<IEnumerable<Trip>> GetAllTripsAsync(CancellationToken ct)
    {
        return await _tripRepository.GetAllTripsAsync(ct);
    }

    public async Task<Trip?> GetTripByIdAsync(string id, CancellationToken ct)
    {
        return await _tripRepository.GetTripAsync(Guid.Parse(id), ct);
    }

    public async Task AddTripAsync(TripDTO tripDto, CancellationToken ct)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            var customer = await _customerRepository.GetCustomerAsync(tripDto.Customer.Contact.Id, ct);
            if (customer == null)
            {
                customer = await _customerRepository.GetCustomerAsync(tripDto.Customer.Contact.Phone, ct);
                if (customer == null)
                {
                    await _contactInfoService.UpsertContactInfoAsync(tripDto.Customer.Contact, ct);
                    
                    customer = _mapper.Map<Customer>(tripDto.Customer);
                    await _customerRepository.UpsertCustomerAsync(customer, ct);
                }
            }
            
            var carrier = await _carrierRepository.GetCarrierAsync(tripDto.Carrier.Contact.Id, ct);
            if (carrier == null)
            {
                carrier = await _carrierRepository.GetCarrierAsync(tripDto.Carrier.Contact.Phone, ct);
                if (carrier == null)
                {
                    await _contactInfoService.UpsertContactInfoAsync(tripDto.Carrier.Contact, ct);
                    
                    carrier = _mapper.Map<Carrier>(tripDto.Carrier);
                    await _carrierRepository.UpsertCarrierAsync(carrier, ct);
                }
            }
            
            var driver = await _driverRepository.GetDriverAsync(tripDto.Driver.Contact.Id, ct);
            if (driver == null)
            {
                driver = await _driverRepository.GetDriverAsync(tripDto.Driver.Contact.Phone, ct);
                if (driver == null)
                {
                    await _contactInfoService.UpsertContactInfoAsync(tripDto.Driver.Contact, ct);
                    
                    driver = _mapper.Map<Driver>(tripDto.Driver);
                    await _driverRepository.UpsertDriverAsync(driver, ct);
                }
            }
            
            var transport = await _transportRepository.GetTransportAsync(tripDto.Transport.LicencePlate, ct);
            if (transport == null)
            {
                transport = _mapper.Map<Transport>(tripDto.Transport);
                await _transportRepository.UpsertTransportAsync(transport, ct);
            }
            
            var trip = _mapper.Map<Trip>(tripDto);
            trip.Customer = customer;
            trip.Carrier = carrier;
            trip.Driver = driver;
            trip.Transport = transport;
            
            await _tripRepository.AddTripAsync(trip, ct);
            
            foreach (var routePointDto in tripDto.RoutePoints)
            {
                await _routePointService.CreateRoutePointAsync(routePointDto, ct);
            }
            
            await transaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
}