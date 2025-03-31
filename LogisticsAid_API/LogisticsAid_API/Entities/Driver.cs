using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAid_API.Entities;

[Table("drivers", Schema = "public")]
public class Driver
{
    [Key]
    [Required]
    [Column("contact_id")]
    public Guid ContactId { get; set; }  
    
    [Required]
    [Column("licence")]
    [MaxLength(50)]
    public required string License { get; set; }
    
    [Required]
    [Column("company_name")]
    [MaxLength(50)]
    public required string CompanyName { get; set; }
    
    // -----Navigation properties-----
    
    [ForeignKey(nameof(ContactId))]
    public required ContactInfo Contact { get; set; }  
}