using API.Contracts;
using API.Contracts.IRepositories;
using API.Contracts.IServices;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/educations")]
public class EducationController : BaseController<Education>
{
    public EducationController(IEducationService service) : base(service)
    {
    }
}