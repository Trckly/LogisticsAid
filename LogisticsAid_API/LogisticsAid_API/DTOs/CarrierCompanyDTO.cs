using LogisticsAid_API.Entities;

namespace LogisticsAid_API.DTOs;

public class CarrierCompanyDTO
{
    public required string CompanyName { get; set; }
    
    public required ICollection<ContactInfo> Contacts { get; set; } = [];
    public required ICollection<Driver> Drivers { get; set; } = [];
    public required ICollection<Transport> Transport { get; set; } = [];
}