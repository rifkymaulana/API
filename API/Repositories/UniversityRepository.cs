using API.Contracts;
using API.Contracts.IRepositories;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : BaseRepository<University>, IUniversityRepository
{
    public UniversityRepository(ApplicationDbContext context) : base(context)
    {
    }
}