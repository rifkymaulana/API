using System.ComponentModel.DataAnnotations;
using API.Models;
using API.Utilities;

namespace API.DTOs.Accounts;

public class GetAccountDto
{
    public Guid EmployeeGuid { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public string Otp { get; set; }
    public bool? IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }
    public static implicit operator Account(GetAccountDto accountDto)
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
    
    public static explicit operator GetAccountDto(Account account)
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
