using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities.Auxiliary;

[Table("contact_info_carrier_company", Schema = "public")]
public class ContactInfoCarrierCompany
{
    [Required]
    [Column("contact_info_id")]
    public required Guid ContactInfoId { get; set; }
    
    [Required]
    [Column("carrier_company_id")]
    [MaxLength(250)]
    public required string CarrierCompanyId { get; set; }
    
    // -----Navigation properties-----
    [ForeignKey(nameof(ContactInfoId))]
    public ContactInfo? ContactInfo { get; set; } = null!;
    
    [ForeignKey(nameof(CarrierCompanyId))]
    public CarrierCompany? CarrierCompany { get; set; } = null!;
}