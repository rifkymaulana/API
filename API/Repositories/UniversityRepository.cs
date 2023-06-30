using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : BaseRepository<University>, IUniversityRepository
{
    public UniversityRepository(AppDbContext context) : base(context)
    {
    }
    
    public IEnumerable<University> GetByName(string name)
    {
        return _context.Universities.Where(u => u.Name == name).ToList();
    }
}