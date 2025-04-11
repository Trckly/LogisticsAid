namespace LogisticsAid_API.DTOs;

public class TransportDTO
{
    public required string LicensePlate { get; set; }
    public required string TruckBrand { get; set; }
    public required string TrailerLicensePlate { get; set; }
    public required string CompanyName { get; set; }
}