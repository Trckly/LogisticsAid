namespace LogisticsAid_API.DTOs;

public class ContactInfoDTO
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }

    public DateTime? BirthDate { get; set; }
}