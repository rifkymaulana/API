using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/educations")]
public class EducationController : BaseController<Education>
{
    public EducationController(IBaseRepository<Education> repository) : base(repository)
    {
    }
}