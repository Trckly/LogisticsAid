using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.DTOs;

public class RoutePointDTO
{
    public required string Id { get; set; }
    public required ERoutePointType Type { get; set; }
    public required int Sequence { get; set; }
    public string? CompanyName { get; set; }
    public string? AdditionalInfo { get; set; }
    public required AddressDTO Address { get; set; }
    public required ICollection<TripDTO> Trips { get; set; }
}