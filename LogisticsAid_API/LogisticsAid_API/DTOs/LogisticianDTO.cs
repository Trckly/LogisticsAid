namespace LogisticsAid_API.DTOs;

public class LogisticianDTO
{
    public required ContactInfoDTO ContactInfo { get; set; }
    
    public string? Password { get; set; }
    public required bool HasAdminPrivileges { get; set; }
}