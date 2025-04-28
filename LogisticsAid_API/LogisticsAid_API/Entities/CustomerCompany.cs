using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}