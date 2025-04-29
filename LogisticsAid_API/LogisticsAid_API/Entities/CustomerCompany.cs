using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LogisticsAid_API.Entities.Auxiliary;

namespace LogisticsAid_API.Entities;

[Table("customer_companies", Schema = "public")]
public class CustomerCompany
{
    [Key]
    [Required]
    [Column("company_name")]
    [MaxLength(250)]
    public required string CompanyName { get; set; }
    
    // -----Navigation properties-----
    public required ICollection<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();
    public required ICollection<ContactInfoCustomerCompany> ContactInfoCustomerCompanies { get; set; } = new List<ContactInfoCustomerCompany>();
}