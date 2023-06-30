using API.Models;

namespace API.Contracts;

public interface IRoleRepository : IBaseRepository<Role>
{
    public Role? GetByName(string name);
}
