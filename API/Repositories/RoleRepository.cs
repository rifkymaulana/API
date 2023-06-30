using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }
    
    public IEnumerable<Role> GetByName(string name)
    {
        return _context.Roles.Where(u => u.Name == name).ToList();
    }
}