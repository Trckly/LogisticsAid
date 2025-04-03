using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("trips", Schema = "public")]
public class Trip
{
    [Key]
    [Required]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("readable_id")]
    [MaxLength(20)]
    public required string ReadableId { get; set; }
    
    [Required]
    [Column("date_created")]
    public required DateTime DateCreated { get; set; }
    
    [Required]
    [Column("loading_date")]
    public required DateTime LoadingDate { get; set; }
    
    [Required]
    [Column("unloading_date")]
    public required DateTime UnloadingDate { get; set; }
    
    [Required]
    [Column("cargo_name")]
    [MaxLength(50)]
    public required string CargoName { get; set; }
    
    [Required]
    [Column("logistician_id")]
    public required Guid LogisticianId { get; set; }
    
    [Required]
    [Column("carrier_id")]
    public required Guid CarrierId { get; set; }
    
    [Required]
    [Column("customer_id")]
    public required Guid CustomerId { get; set; }
    
    [Required]
    [Column("driver_id")]
    public required Guid DriverId { get; set; }
    
    [Required]
    [Column("transport_id")]
    [MaxLength(8)]
    public required string TransportId { get; set; }
    
    [Required]
    [Column("price")]
    public decimal Price { get; set; }

    
    
    // -----Navigation properties-----

    [ForeignKey(nameof(LogisticianId))] 
    public required Logistician Logistician { get; set; }

    [ForeignKey(nameof(CarrierId))]
    public required Carrier Carrier { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public required Customer Customer { get; set; }
    
    [ForeignKey(nameof(DriverId))]
    public required Driver Driver { get; set; }
    
    [ForeignKey(nameof(TransportId))]
    public required Transport Transport { get; set; }
    
    public ICollection<RoutePoint> RoutePoints { get; set; } = new List<RoutePoint>();
}