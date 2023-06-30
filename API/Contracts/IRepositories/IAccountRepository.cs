using API.DTOs.Accounts;
using API.Models;

namespace API.Contracts;

public interface IAccountRepository : IBaseRepository<Account>
{
    public GetAccountDto? LoginAccount(string email, string password);
}
