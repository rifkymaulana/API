namespace API.DTOs.Accounts;

public class UpdateAccountDto
{
    public Guid EmployeeGuid { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
}