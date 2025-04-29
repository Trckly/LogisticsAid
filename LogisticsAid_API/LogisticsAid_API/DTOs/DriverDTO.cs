using LogisticsAid_API.Entities;

namespace LogisticsAid_API.DTOs;

public class DriverDTO
{
    public required string License { get; set; }
    public required ContactInfoDTO ContactInfo { get; set; }
    
    public required CarrierCompanyDTO CarrierCompany { get; set; }
}