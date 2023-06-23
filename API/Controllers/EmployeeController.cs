using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : BaseController<Employee>
{
    public EmployeeController(IBaseRepository<Employee> repository) : base(repository)
    {
    }
}