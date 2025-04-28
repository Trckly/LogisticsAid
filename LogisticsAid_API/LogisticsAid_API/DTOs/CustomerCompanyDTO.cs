using LogisticsAid_API.Entities;

namespace LogisticsAid_API.DTOs;

public class CustomerCompanyDTO
{
    public required string CompanyName { get; set; }
    
    public required ICollection<ContactInfo> Contacts { get; set; } = [];
}