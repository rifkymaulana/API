using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : BaseRepository<University>, IUniversityRepository
{
    public UniversityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IEnumerable<University>? GetByName(string name)
    {
        return _context.Set<University>().Where(u => u.Name.Contains(name)).ToList();
    }
}