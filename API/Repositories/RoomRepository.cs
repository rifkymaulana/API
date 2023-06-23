using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext context) : base(context)
    {
    }
}