namespace LogisticsAid_API.DTOs;

public class CustomerDTO
{
    public required ContactInfoDTO Contact { get; set; }
    public required string CompanyName { get; set; }
}