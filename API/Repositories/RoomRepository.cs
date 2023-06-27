using API.Contracts;
using API.Contracts.IRepositories;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext context) : base(context)
    {
    }
}