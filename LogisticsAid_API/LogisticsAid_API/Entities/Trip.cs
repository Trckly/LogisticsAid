using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LogisticsAid_API.Entities.Auxiliary;

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
    [Column("cargo_weight")]
    public required decimal CargoWeight { get; set; }
    
    [Required]
    [Column("logistician_id")]
    public required Guid LogisticianId { get; set; }
    
    [Required]
    [Column("carrier_company_id")]
    [MaxLength(100)]
    public required string CarrierCompanyId { get; set; }
    
    [Required]
    [Column("customer_company_id")]
    [MaxLength(100)]
    public required string CustomerCompanyId { get; set; }
    
    [Required]
    [Column("driver_id")]
    public required Guid DriverId { get; set; }
    
    [Required]
    [Column("truck_id")]
    [MaxLength(8)]
    public required string TruckId { get; set; }
    
    [Required]
    [Column("trailer_id")]
    [MaxLength(8)]
    public required string TrailerId { get; set; }
    
    [Required]
    [Column("customer_price")]
    public decimal CustomerPrice { get; set; }
    
    [Required]
    [Column("carrier_price")]
    public decimal CarrierPrice { get; set; }
    
    [Required]
    [Column("with_tax")]
    public required bool WithTax { get; set; }

    
    
    // -----Navigation properties-----

    [ForeignKey(nameof(LogisticianId))] 
    public required Logistician Logistician { get; set; }

    [ForeignKey(nameof(CarrierCompanyId))]
    public required CarrierCompany CarrierCompany { get; set; }
    
    [ForeignKey(nameof(CustomerCompanyId))]
    public required CustomerCompany CustomerCompany { get; set; }
    
    [ForeignKey(nameof(DriverId))]
    public required Driver Driver { get; set; }
    
    [ForeignKey(nameof(TruckId))]
    public required Transport Truck { get; set; }
    
    [ForeignKey(nameof(TrailerId))]
    public required Transport Trailer { get; set; }
    
    public ICollection<RoutePoint> RoutePoints { get; set; } = new List<RoutePoint>();
    public ICollection<RoutePointTrip> RoutePointTrips { get; set; } = new List<RoutePointTrip>();
}