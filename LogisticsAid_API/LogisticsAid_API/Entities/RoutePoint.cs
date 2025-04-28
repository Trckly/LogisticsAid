using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LogisticsAid_API.Entities.Auxiliary;
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
    [Column("address_id")]
    public required Guid AddressId { get; set; }

    [Required]
    [Column("type")]
    public required ERoutePointType Type { get; set; }

    [Required]
    [Column("sequence")]
    public required int Sequence { get; set; }
    
    // -----Optional-----
    
    [Column("company_name")]
    [MaxLength(100)]
    public string? CompanyName { get; set; }
    
    
    [Column("additional_info")]
    [MaxLength(500)]
    public string? AdditionalInfo { get; set; }

    // -----Navigation properties-----
    [ForeignKey(nameof(AddressId))]
    public required Address Address { get; set; }

    public required ICollection<Trip> Trips { get; set; } = new List<Trip>();
    public required ICollection<RoutePointTrip> RoutePointTrips { get; set; } = new List<RoutePointTrip>();
}