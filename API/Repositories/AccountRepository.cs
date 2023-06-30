using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.Models;

namespace API.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext context) : base(context)
    {
    }

    public GetAccountDto? LoginAccount(string email, string password)
    {
        // login
        var entity = _context.Employees.FirstOrDefault(u => u.Email == email);
        if (entity == null) return null;
        var entityAccount = _context.Accounts.FirstOrDefault(u => u.Guid == entity.Guid);
        
        var entityDto = new GetAccountDto()
        {
            EmployeeGuid = entity.Guid,
            Password = entityAccount.Password,
        };
        
        return entityDto;
    }
}
