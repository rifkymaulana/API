using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IBaseRepository<University>
{
    public IEnumerable<University>? GetByName(string name);
}