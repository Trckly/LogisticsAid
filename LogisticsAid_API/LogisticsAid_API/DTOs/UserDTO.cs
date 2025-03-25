namespace LogisticsAid_API.DTOs;

public class UserDTO
{
    // --- ContactInfo model ---
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Phone { get; set; }
    
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }
    
    // --- Logistician model ---
    public string? Password { get; set; }
    public required bool HasAdminPrivileges { get; set; }
}