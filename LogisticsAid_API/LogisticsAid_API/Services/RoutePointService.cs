using AutoMapper;
using LogisticsAid_API.Context;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories;
using LogisticsAid_API.Repositories.Interfaces;

namespace LogisticsAid_API.Services;

public class RoutePointService
{
    private readonly LogisticsAidDbContext _context;
    private readonly IMapper _mapper;
    private readonly IRoutePointRepository _routePointRepository;
    private readonly AddressService _addressService;
    private readonly ContactInfoService _contactInfoService;

    public RoutePointService(LogisticsAidDbContext context, IMapper mapper,
        IRoutePointRepository routePointRepository, AddressService addressService, ContactInfoService contactInfoService)
    {
        _context = context;
        _mapper = mapper;
        _routePointRepository = routePointRepository;
        _addressService = addressService;
        _contactInfoService = contactInfoService;
    }

    public async Task CreateRoutePointAsync(RoutePointDTO routePointDto, CancellationToken ct)
    {
        await _addressService.UpsertAddressAsync(routePointDto.Address, ct);
        var addressDto = await _addressService.GetAddressAsync(routePointDto.Address, ct);
        
        var routePoint = _mapper.Map<RoutePoint>(routePointDto);
        routePoint.AddressId = Guid.Parse(addressDto.Id);           // Actual address id if it already existed prior to this function call
        await _routePointRepository.CreateRoutePointAsync(routePoint, ct);
    }

    public async Task<IEnumerable<RoutePointDTO>> GetRoutePointsByIdAsync(IEnumerable<string> routePointIds,
        CancellationToken ct)
    {
        var routePoints = 
            await _routePointRepository.GetRoutePointsByIdAsync(routePointIds.Select(Guid.Parse), ct);
        
        return routePoints.Select(rp => _mapper.Map<RoutePointDTO>(rp));
    }


}