using API.Models;

namespace API.Contracts;

public interface IEducationRepository : IBaseRepository<Education>
{
    public IEnumerable<Education>? GetByName(string name);
}