using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("logisticians", Schema = "public")]
public class Logistician
{
    [Key]
    [Required]
    [Column("contact_id")]
    public Guid ContactId { get; set; }  

    [Required]
    [Column("password_salt")]
    [MaxLength(64)]
    public required string PasswordSalt { get; set; }  

    [Required]
    [Column("password_hash")]
    [MaxLength(128)]
    public required string PasswordHash { get; set; }  

    [Required]
    [Column("admin_privileges")]
    public required bool HasAdminPrivileges { get; set; }
    
    // -----Navigation properties-----

    [ForeignKey(nameof(ContactId))]
    public required ContactInfo ContactInfo { get; set; }  
}