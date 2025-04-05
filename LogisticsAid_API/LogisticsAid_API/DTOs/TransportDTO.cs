namespace LogisticsAid_API.DTOs;

public class TransportDTO
{
    public required string LicencePlate { get; set; }
    public required string TruckBrand { get; set; }
    public required string TrailerLicencePlate { get; set; }
    public required string CompanyName { get; set; }
}