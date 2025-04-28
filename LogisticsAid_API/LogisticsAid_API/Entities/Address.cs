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
    [Column("city")]
    [MaxLength(50)]
    public required string City { get; set; }
    
    // -----Optional-----
    
    [Column("number")]
    [MaxLength(10)]
    public string? Number { get; set; }
    
    [Column("street")]
    [MaxLength(100)]
    public string? Street { get; set; }
    
    [Column("province")]
    [MaxLength(50)]
    public string? Province { get; set; }

    [Column("country")]
    [MaxLength(50)]
    public string? Country { get; set; }
    
    
    [Column("latitude")]
    public double? Latitude { get; set; }

    [Column("longitude")]
    public double? Longitude { get; set; }
}