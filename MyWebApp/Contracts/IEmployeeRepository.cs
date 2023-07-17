using API.DTOs.Employees;

namespace MyWebApp.Contracts;

public interface IEmployeeRepository : IBaseRepository<GetEmployeeDto, Guid>
{
}
