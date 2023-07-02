using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Accounts;

public class UpdateAccountDto
{
    [Required]
    public Guid EmployeeGuid { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public bool IsDeleted { get; set; }
}
