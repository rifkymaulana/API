using API.Contracts;
using API.Data;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : BaseController<Account>
{
    public AccountController(IAccountRepository repository) : base(repository)
    {
    }
}