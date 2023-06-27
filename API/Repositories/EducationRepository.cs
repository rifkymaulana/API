using API.Contracts;
using API.Contracts.IRepositories;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EducationRepository : BaseRepository<Education>, IEducationRepository
{
    public EducationRepository(ApplicationDbContext context) : base(context)
    {
    }
}