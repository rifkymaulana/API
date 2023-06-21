using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_educations")]
public class Education
{
    [Key]
    [Column("guid")]
    public Guid Guid { get; set; }

    [ForeignKey("university_guid")]
    public Guid UniversityGuid { get; set; }
    
    [Column("major", TypeName = "nvarchar(100)")]
    public string Major { get; set; }
    
    [Column("gpa")]
    public decimal Gpa { get; set; }

    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }   
}