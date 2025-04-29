using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LogisticsAid_API.Entities.Auxiliary;

namespace LogisticsAid_API.Entities;

[Table("carrier_companies", Schema = "public")]
public class CarrierCompany
{
    [Key]
    [Required]
    [Column("company_name")]
    [MaxLength(250)]
    public required string CompanyName { get; set; }
    
    // -----Navigation properties-----
    public required ICollection<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();
    public required ICollection<ContactInfoCarrierCompany> ContactInfoCarrierCompany { get; set; } = new List<ContactInfoCarrierCompany>();
}