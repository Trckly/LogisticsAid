using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LogisticsAid_API.Entities.Enums;

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
    [Column("type")]
    public required ETransportType TransportType { get; set; }
    
    [Required]
    [Column("carrier_company_id")]
    [MaxLength(250)]
    public required string CarrierCompanyId { get; set; }
    
    // -----Optional-----
    
    [Column("brand")]
    [MaxLength(100)]
    public string? Brand { get; set; }
    
    // -----Navigation properties-----
    [ForeignKey(nameof(CarrierCompanyId))]
    public required CarrierCompany CarrierCompany { get; set; }
}