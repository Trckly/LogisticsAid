using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities.Auxiliary;

[Table("contact_info_customer_company", Schema = "public")]
public class ContactInfoCustomerCompany
{
    [Required]
    [Column("contact_info_id")]
    public required Guid ContactInfoId { get; set; }
    
    [Required]
    [Column("customer_company_id")]
    [MaxLength(250)]
    public required string CustomerCompanyId { get; set; }
    
    // -----Navigation properties-----
    [ForeignKey(nameof(ContactInfoId))]
    public ContactInfo? ContactInfo { get; set; }
    
    [ForeignKey(nameof(CustomerCompanyId))]
    public CustomerCompany? CustomerCompany { get; set; }
}