using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities.Auxiliary;

[Table("route_point_trip", Schema = "public") ]
public class RoutePointTrip
{
    [Required]
    [Column("route_point_id")]
    public Guid RoutePointId { get; set; }
    
    [Required]
    [Column("trip_id")]
    public Guid TripId { get; set; }
    
    // -----Navigation properties-----
    [ForeignKey(nameof(RoutePointId))]
    public RoutePoint RoutePoint { get; set; } = null!;
    
    [ForeignKey(nameof(TripId))]
    public Trip Trip { get; set; } = null!;
}