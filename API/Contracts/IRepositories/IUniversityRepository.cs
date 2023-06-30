using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IBaseRepository<University>
{
    public IEnumerable<University>? GetByName(string name);
    University? CreateWithDuplicateCheck(University university);
}
