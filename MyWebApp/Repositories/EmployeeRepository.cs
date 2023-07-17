using API.DTOs.Employees;
using MyWebApp.Contracts;

namespace MyWebApp.Repositories;

public class EmployeeRepository : BaseRepository<GetEmployeeDto, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request="employees/") : base(request)
    {
    }
}
