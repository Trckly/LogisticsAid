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
    
    [Required]
    [Column("email")]
    [MaxLength(254)]
    public required string Email { get; set; }
    
    
    // -----Optional-----
    
    [Column("customer_company_id")]
    [MaxLength(50)]
    public string? CustomerCompanyId { get; set; }
    
    [Column("carrier_company_id")]
    [MaxLength(50)]
    public string? CarrierCompanyId { get; set; }
    
    [Column("birth_date")]
    public DateTime? BirthDate { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(CustomerCompanyId))]
    public CustomerCompany? CustomerCompany { get; set; }
    
    [ForeignKey(nameof(CarrierCompanyId))]
    public CarrierCompany? CarrierCompany { get; set; }
    
}