

using AutoMapper;
using LogisticsAid_API.Context;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Auxiliary;
using LogisticsAid_API.Exceptions;
using LogisticsAid_API.Repositories.Interfaces;

namespace LogisticsAid_API.Services;

public class TripService
{
    private readonly LogisticsAidDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITripRepository _tripRepository;
    private readonly ICustomerCompanyRepository _customerCompanyRepository;
    private readonly ICarrierCompanyRepository _carrierCompanyRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly ITransportRepository _transportRepository;
    private readonly RoutePointService _routePointService;
    private readonly ContactInfoService _contactInfoService;
    private readonly AddressService _addressService;
    private readonly RoutePointTripService _routePointTripService;

    public TripService(LogisticsAidDbContext context, IMapper mapper, ITripRepository tripRepository, 
        ICustomerCompanyRepository customerCompanyRepository, ICarrierCompanyRepository carrierCompanyRepository, IDriverRepository driverRepository, 
        ITransportRepository transportRepository, RoutePointService routePointService, 
        ContactInfoService contactInfoService, AddressService addressService, RoutePointTripService routePointTripService)
    {
        _context = context;
        _mapper = mapper;
        _tripRepository = tripRepository;
        _customerCompanyRepository = customerCompanyRepository;
        _carrierCompanyRepository = carrierCompanyRepository;
        _driverRepository = driverRepository;
        _transportRepository = transportRepository;
        _routePointService = routePointService;
        _contactInfoService = contactInfoService;
        _addressService = addressService;
        _routePointTripService = routePointTripService;
    }

    public async Task<IEnumerable<TripDTO>> GetAllTripsAsync(CancellationToken ct)
    {
        var trips = await _tripRepository.GetAllTripsAsync(ct);
        var tripsDto = trips.Select(trip => _mapper.Map<TripDTO>(trip));
        return tripsDto;
    }

    public async Task<Trip?> GetTripByIdAsync(string id, CancellationToken ct)
    {
        return await _tripRepository.GetTripAsync(Guid.Parse(id), ct);
    }
    
    public async Task<Trip?> GetTripByReadableIdAsync(string readableId, CancellationToken ct)
    {
        return await _tripRepository.GetTripAsync(readableId, ct);
    }

    public async Task<IEnumerable<TripDTO>> GetTripsAsync(int page, int pageSize, CancellationToken ct)
    {
        var trips = await _tripRepository.GetTripsAsync(page, pageSize, ct);
        return trips.Select(trip => _mapper.Map<TripDTO>(trip));
    }

    public async Task AddTripAsync(TripDTO tripDto, CancellationToken ct)
    {
        var existingTrip = await _tripRepository.GetTripAsync(tripDto.ReadableId, ct);
        if (existingTrip != null)
        {
            throw new TripAlreadyExistsException(existingTrip.ReadableId);
        }
        
        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            var customer = await _customerCompanyRepository.GetCustomerCompanyAsync(tripDto.CustomerCompany.CompanyName, ct);
            if (customer == null)
            {
                customer = _mapper.Map<CustomerCompany>(tripDto.CustomerCompany);
                await _customerCompanyRepository.UpsertCustomerCompanyAsync(customer, ct);
            }

            var carrier = await _carrierCompanyRepository.GetCarrierAsync(tripDto.CarrierCompany.CompanyName, ct);
            if (carrier == null)
            {
                carrier = _mapper.Map<CarrierCompany>(tripDto.CarrierCompany);
                await _carrierCompanyRepository.UpsertCarrierAsync(carrier, ct);
            }

            // First check if driver exists by ID or phone
            var driver = await _driverRepository.GetDriverAsync(tripDto.Driver.ContactInfo.Id, ct);
            if (driver == null)
            {
                driver = await _driverRepository.GetDriverAsync(tripDto.Driver.ContactInfo.Phone, ct);
                if (driver == null)
                {
                    // Get existing contact info
                    var contactInfo = await _context.ContactInfo.FindAsync([Guid.Parse(tripDto.Driver.ContactInfo.Id)], ct);
                    if (contactInfo == null)
                    {
                        // Create new contact info if it doesn't exist
                        await _contactInfoService.UpsertContactInfoAsync(tripDto.Driver.ContactInfo, ct);
                    }
        
                    // Create new driver with reference to existing contact info ID
                    driver = _mapper.Map<Driver>(tripDto.Driver);
                    driver.CarrierCompany = carrier;
                    driver.ContactId = Guid.Parse(tripDto.Driver.ContactInfo.Id); // Just set the ID reference
                    driver.ContactInfo = null; // Don't include the navigation property to avoid tracking issues
                    await _driverRepository.UpsertDriverAsync(driver, ct);
                }
            }
            
            var truck = await _transportRepository.GetTransportAsync(tripDto.Truck.LicensePlate, ct);
            if (truck == null)
            {
                truck = _mapper.Map<Transport>(tripDto.Truck);
                truck.CarrierCompany = carrier;
                await _transportRepository.UpsertTransportAsync(truck, ct);
            }
            
            var trailer = await _transportRepository.GetTransportAsync(tripDto.Trailer.LicensePlate, ct);
            if (trailer == null)
            {
                trailer = _mapper.Map<Transport>(tripDto.Trailer);
                trailer.CarrierCompany = carrier;
                await _transportRepository.UpsertTransportAsync(trailer, ct);
            }
            
            var trip = _mapper.Map<Trip>(tripDto);
            trip.CustomerCompany = customer;
            trip.CarrierCompany = carrier;
            trip.Driver = driver;
            trip.Truck = truck;
            trip.Trailer = trailer;
            
            await _tripRepository.AddTripAsync(trip, ct);
            
            foreach (var routePointDto in tripDto.RoutePoints)
            {
                var existingRoutePointDto = await _routePointService.CreateRoutePointAsync(routePointDto, ct);
                await _routePointTripService.BindRoutePointToTripAsync(existingRoutePointDto, tripDto, ct);
            }
            
            await transaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<int> CountAsync(CancellationToken ct)
    {
        return await _tripRepository.CountAsync(ct);
    }
    
    public async Task DeleteTripsAsync(IEnumerable<string> tripIds, CancellationToken ct)
    {
        await _tripRepository.DeleteTripsAsync(tripIds.Select(Guid.Parse), ct);
    }
}