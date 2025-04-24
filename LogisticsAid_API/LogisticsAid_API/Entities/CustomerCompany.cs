using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("customers", Schema = "public")]
public class Customer
{
    [Key]
    [Required]
    [Column("contact_id")]
    public Guid ContactId { get; set; }  
    
    [Required]
    [Column("company_name")]
    [MaxLength(50)]
    public required string CompanyName { get; set; }
    
    // -----Navigation properties-----
    
    [ForeignKey(nameof(ContactId))]
    public required ContactInfo Contact { get; set; }
}