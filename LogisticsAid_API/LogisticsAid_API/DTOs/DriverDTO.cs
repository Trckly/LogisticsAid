namespace LogisticsAid_API.DTOs;

public class DriverDTO
{
    public required string License { get; set; }
    public required string CompanyName { get; set; }
    public required ContactInfoDTO Contact { get; set; }
}