using System.Net;
using API.Contracts;
using API.Contracts.IRepositories;
using API.Contracts.IServices;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/universities")]
public class UniversityController : BaseController<University>
{
    public UniversityController(IUniversityService service) : base(service)
    {
    }
}