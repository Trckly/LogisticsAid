using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public required ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    public required ICollection<Transport> Transport { get; set; } = new List<Transport>();
}