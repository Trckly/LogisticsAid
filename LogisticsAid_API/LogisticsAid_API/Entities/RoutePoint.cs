using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.Entities;

[Table("route_points", Schema = "public")]
public class RoutePoint
{
    [Key]
    [Required]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("order_id")]
    public required Guid TripId { get; set; }

    [Required]
    [Column("address_id")]
    public required Guid AddressId { get; set; }
    
    [Required]
    [Column("company_name")]
    [MaxLength(100)]
    public required string CompanyName { get; set; }

    [Required]
    [Column("type")]
    public required ERoutePointType Type { get; set; }

    [Required]
    [Column("sequence")]
    public required int Sequence { get; set; }
    
    [Required]
    [Column("contact_info_id")]
    public Guid? ContactInfoId { get; set; }

    // -----Navigation properties-----
     
    [ForeignKey(nameof(TripId))]
    public required Trip Trip { get; set; }

    [ForeignKey(nameof(AddressId))]
    public required Address Address { get; set; }
    
    [ForeignKey(nameof(ContactInfoId))]
    public required ContactInfo ContactInfo { get; set; }
}