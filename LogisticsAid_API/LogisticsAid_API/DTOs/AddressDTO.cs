namespace LogisticsAid_API.DTOs;

public class AddressDTO
{
    public required string Id { get; set; }
    public required string City { get; set; }
    public string? Number { get; set; }
    public string? Street { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}