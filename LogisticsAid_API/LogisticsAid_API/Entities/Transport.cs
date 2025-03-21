using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("transport", Schema = "public")]
public class Transport
{
    [Key]
    [Required]
    [Column("licence_plate")]
    [MaxLength(8)]
    public required string LicencePlate { get; set; }
    
    [Required]
    [Column("truck_brand")]
    [MaxLength(50)]
    public required string TruckBrand { get; set; }
    
    [Required]
    [Column("trailer_licence_plate")]
    [MaxLength(8)]
    public required string TrailerLicencePlate { get; set; }
}