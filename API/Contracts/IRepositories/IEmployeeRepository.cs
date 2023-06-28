using API.Models;

namespace API.Contracts;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    public IEnumerable<Employee>? GetByName(string name);
}