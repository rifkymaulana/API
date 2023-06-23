using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/universities")]
public class UniversityController : BaseController<University>
{
    public UniversityController(IBaseRepository<University> repository) : base(repository)
    {
    }
}