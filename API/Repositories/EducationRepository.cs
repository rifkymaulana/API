using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EducationRepository : BaseRepository<Education>, IEducationRepository
{
    public EducationRepository(ApplicationDbContext context) : base(context)
    {
    }
}