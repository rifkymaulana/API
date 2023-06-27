using API.Contracts;
using API.Contracts.IRepositories;
using API.Data;
using API.Models;

namespace API.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context) : base(context)
    {
    }
}