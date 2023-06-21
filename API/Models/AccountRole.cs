using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_account_roles")]
public class AccountRole
{
    [Key]
    [Column("guid")]
    public Guid Guid { get; set; }
    
    [ForeignKey("account_guid")]
    public Guid AccountGuid { get; set; }
    
    [ForeignKey("role_guid")]
    public Guid RoleGuid { get; set; }
    
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }
}