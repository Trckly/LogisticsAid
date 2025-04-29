using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LogisticsAid_API.Entities.Auxiliary;
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
    
    [Column("birth_date")]
    public DateTime? BirthDate { get; set; }
    
    // Navigation properties
    public required ICollection<CustomerCompany> CustomerCompanies { get; set; } = new List<CustomerCompany>();
    public required ICollection<ContactInfoCustomerCompany> ContactInfoCustomerCompany { get; set; } = new List<ContactInfoCustomerCompany>();
    
    public required ICollection<CarrierCompany> CarrierCompanies { get; set; } = new List<CarrierCompany>();
    public required ICollection<ContactInfoCarrierCompany> ContactInfoCarrierCompany { get; set; } = new List<ContactInfoCarrierCompany>();
}