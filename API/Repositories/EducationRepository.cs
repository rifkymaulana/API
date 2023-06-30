using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EducationRepository : BaseRepository<Education>, IEducationRepository
{
    public EducationRepository(AppDbContext context) : base(context)
    {
    }
    
    public IEnumerable<Education> GetByName(string name)
    {
        return _context.Educations.Where(u => u.Major == name).ToList();
    }
}