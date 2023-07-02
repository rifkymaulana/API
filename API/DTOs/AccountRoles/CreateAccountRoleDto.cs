using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.DTOs.AccountRoles;

public class CreateAccountRoleDto
{
    public Guid Guid { get; set; }
    [Required]
    public Guid AccountGuid { get; set; }
    [Required]
    public Guid RoleGuid { get; set; }
    
    public static implicit operator AccountRole(CreateAccountRoleDto accountRoleDto)
    {
        return new() {
            Guid = accountRoleDto.Guid,
            AccountGuid = accountRoleDto.AccountGuid,
            RoleGuid = accountRoleDto.RoleGuid
        };
    }
    
    public static explicit operator CreateAccountRoleDto(AccountRole accountRoleDto)
    {
        return new() {
            Guid = accountRoleDto.Guid,
            AccountGuid = accountRoleDto.AccountGuid,
            RoleGuid = accountRoleDto.RoleGuid
        };
    }
}
