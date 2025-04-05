namespace LogisticsAid_API.DTOs;

public class AddressDTO
{
    public required string Id { get; set; }
    public required string Number { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Province { get; set; }
    public required string Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}