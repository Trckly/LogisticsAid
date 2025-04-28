using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.DTOs;

public class TransportDTO
{
    public required string LicensePlate { get; set; }
    public required ETransportType TransportType { get; set; }
    
    public string? Brand { get; set; }
    
    public required CarrierCompanyDTO CarrierCompany { get; set; }
}