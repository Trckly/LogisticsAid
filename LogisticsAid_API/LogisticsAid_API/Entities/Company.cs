using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("companies", Schema = "public")]
public class Company
{
    [Key]
    [Required]
    [Column("company_name")]
    [MaxLength(50)]
    public required string CompanyName { get; set; }
    
    // Requisites can be implemented additionally or taxing system preferred, etc.
    
    // -----Navigation properties-----
}