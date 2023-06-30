using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : BaseRepository<University>, IUniversityRepository
{
    public UniversityRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public IEnumerable<University> GetByName(string name)
    {
        return _context.Universities.Where(u => u.Name == name).ToList();
    }
    
    public University? CreateWithDuplicateCheck(University university)
    {
        var getUniversity = _context.Universities.FirstOrDefault(u => u.Name == university.Name && u.Code == university.Code);
        
        if (getUniversity != null)
        {
            return getUniversity;
        }
        
        return Create(university);
    }
}
