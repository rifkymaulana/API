using API.Contracts;
using API.Contracts.IRepositories;
using API.Contracts.IServices;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : BaseController<Role>
{
    public RolesController(IRoleService service) : base(service)
    {
    }
}