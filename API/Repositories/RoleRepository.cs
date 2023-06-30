using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public Role? GetByName(string name)
    {
        return _context.Set<Role>().FirstOrDefault(r => r.Name == name);
    }
}
