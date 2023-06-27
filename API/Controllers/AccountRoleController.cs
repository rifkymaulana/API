using API.Contracts;
using API.Contracts.IRepositories;
using API.Contracts.IServices;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/account-roles")]
public class AccountRoleController : BaseController<AccountRole>
{
    public AccountRoleController(IAccountRoleService service) : base(service)
    {
    }
}