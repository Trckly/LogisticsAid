using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.Entities;

[Table("contact_info", Schema = "public")]
public class ContactInfo
{
    [Key]
    [Required]
    [Column("id")]
    public required Guid Id { get; set; }
    
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
    
    // -----Optional-----
    
    [Column("birth_date")]
    public DateOnly? BirthDate { get; set; }
    
    [Column("email")]
    [MaxLength(254)]
    public string? Email { get; set; }
}