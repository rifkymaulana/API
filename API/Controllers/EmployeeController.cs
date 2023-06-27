using API.Contracts;
using API.Contracts.IRepositories;
using API.Contracts.IServices;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : BaseController<Employee>
{
    public EmployeeController(IEmployeeService service) : base(service)
    {
    }
}