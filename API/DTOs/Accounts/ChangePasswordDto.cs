using System.ComponentModel.DataAnnotations;
using API.Utilities;

namespace API.DTOs.Accounts;

public class ChangePasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Otp { get; set; }
    [Required]
    [PasswordPolicy]
    public string NewPassword { get; set; }
    [Required]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
    public string ConfirmNewPassword { get; set; }
}
