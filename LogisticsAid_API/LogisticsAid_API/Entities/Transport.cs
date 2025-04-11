using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("transport", Schema = "public")]
public class Transport
{
    [Key]
    [Required]
    [Column("license_plate")]
    [MaxLength(8)]
    public required string LicensePlate { get; set; }
    
    [Required]
    [Column("truck_brand")]
    [MaxLength(50)]
    public required string TruckBrand { get; set; }
    
    [Required]
    [Column("trailer_license_plate")]
    [MaxLength(8)]
    public required string TrailerLicensePlate { get; set; }
    
    [Required]
    [Column("company_name")]
    [MaxLength(50)]
    public required string CompanyName { get; set; }
    
    // -----Navigation properties-----
}