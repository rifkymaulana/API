namespace API.DTOs.Accounts;

public class ChangePasswordDto
{
    public string Email { get; set; }
    public string Otp { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
