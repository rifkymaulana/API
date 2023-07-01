using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class AccountRoleRepository : BaseRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid)
    {
        return _context.Set<AccountRole>().Where(ar => ar.AccountGuid == guid);
    }
}
