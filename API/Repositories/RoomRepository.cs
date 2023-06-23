using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoomRepository : BaseRepository<Room>
{
    public RoomRepository(ApplicationDbContext context) : base(context)
    {
    }
}