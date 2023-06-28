using API.Models;

namespace API.Contracts;

public interface IRoomRepository : IBaseRepository<Room>
{
    public IEnumerable<Room>? GetByName(string name);
}