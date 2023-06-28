using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AccountRoles;

public class CreateAccountRoleDto
{
    [Required]
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }
}
