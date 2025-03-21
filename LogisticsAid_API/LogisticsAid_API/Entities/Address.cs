using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("addresses", Schema = "public")]
public class Address
{
    [Key]
    [Required]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("number")]
    [MaxLength(10)]
    public required string Number { get; set; }

    [Required]
    [Column("street")]
    [MaxLength(100)]
    public required string Street { get; set; }

    [Required]
    [Column("city")]
    [MaxLength(50)]
    public required string City { get; set; }
    
    [Required]
    [Column("province")]
    [MaxLength(50)]
    public required string Province { get; set; }

    [Required]
    [Column("country")]
    [MaxLength(50)]
    public required string Country { get; set; }

    // Optional: latitude and longitude for geolocation
    [Column("latitude")]
    public double? Latitude { get; set; }

    [Column("longitude")]
    public double? Longitude { get; set; }
}