using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("logisticians", Schema = "public")]
public class Logistician
{
    [Key]
    [Required]
    [Column("email")]
    [MaxLength(254)]
    public required string Email { get; set; }
    
    [Required]
    [Column("first_name")]
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [Required]
    [Column("last_name")]
    [MaxLength(50)]
    public required string LastName { get; set; }
    
    [Required]
    [Column("phone")]
    [MaxLength(13)]
    [MinLength(13)]
    public required string Phone { get; set; }
    
    [Required]
    [Column("birth_date")]
    public required DateOnly BirthDate { get; set; }
    
    [Required]
    [Column("gender")]
    public required EGender Gender { get; set; }
    
    [Required]
    [Column("password_salt")]
    [MaxLength(64)]
    public string? PasswordSalt { get; set; }
    
    [Required]
    [Column("password_hash")]
    [MaxLength(128)]
    public string? PasswordHash { get; set; }
    
    [Required]
    [Column("admin_privileges")]
    public required bool HasAdminPrivileges { get; set; }
}