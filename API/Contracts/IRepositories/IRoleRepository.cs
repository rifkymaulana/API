using API.Models;

namespace API.Contracts;

public interface IRoleRepository : IBaseRepository<Role>
{
    public IEnumerable<Role>? GetByName(string name);
}