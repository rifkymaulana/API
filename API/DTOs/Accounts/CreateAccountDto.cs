using API.Models;

namespace API.DTOs.Accounts;

public class CreateAccountDto
{
    public Guid EmployeeGuid { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public string? Otp { get; set; }
    public bool? IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }
    
    public static implicit operator Account(CreateAccountDto accountDto)
    {
        return new() {
            Guid = accountDto.EmployeeGuid,
            Password = accountDto.Password,
            IsDeleted = accountDto.IsDeleted,
            Otp = accountDto.Otp,
            IsUsed = accountDto.IsUsed,
            ExpiredTime = accountDto.ExpiredTime
        };
    }
    
    public static explicit operator CreateAccountDto(Account account)
    {
        return new() {
            EmployeeGuid = account.Guid,
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }
}
