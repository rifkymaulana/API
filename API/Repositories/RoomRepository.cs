using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public IEnumerable<Room> GetByName(string name)
    {
        return _context.Rooms.Where(u => u.Name == name).ToList();
    }
}