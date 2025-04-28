using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("drivers", Schema = "public")]
public class Driver
{
    [Key]
    [Required]
    [Column("contact_id")]
    public Guid ContactId { get; set; }  
    
    [Required]
    [Column("licence")]
    [MaxLength(50)]
    public required string License { get; set; }
    
    [Required]
    [Column("carrier_company_id")]
    [MaxLength(100)]
    public required string CarrierCompanyId { get; set; }

    
    // -----Navigation properties-----
    
    [ForeignKey(nameof(ContactId))]
    public ContactInfo? ContactInfo { get; set; }  
    
    [ForeignKey(nameof(CarrierCompanyId))]
    public CarrierCompany? CarrierCompany { get; set; }
}