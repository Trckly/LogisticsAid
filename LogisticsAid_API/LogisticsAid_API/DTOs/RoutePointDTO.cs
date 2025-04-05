using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.DTOs;

public class RoutePointDTO
{
    public required Guid Id { get; set; }
    public required Guid TripId { get; set; }
    public required AddressDTO Address { get; set; }
    public required string CompanyName { get; set; }
    public required ERoutePointType Type { get; set; }
    public required int Sequence { get; set; }
    public required ContactInfoDTO ContactInfo { get; set; }
}