using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _repository;
    
    public RolesController(IRoleRepository repository)
    {
        _repository = repository;  
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var roles = _repository.GetAll();
        if (!roles.Any()) return NotFound();
        return Ok(roles);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var role = _repository.GetByGuid(guid);
        if (role is null) return NotFound();
        return Ok(role);
    }
    
    [HttpPost]
    public IActionResult Create(Role role)
    {
        var isCreated = _repository.Create(role);
        return Ok(isCreated);
    }
    
    [HttpPut]
    public IActionResult Update(Role role)
    {
        var isUpdated = _repository.Update(role);
        if (!isUpdated) return NotFound();
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _repository.Delete(guid);
        if (!isDeleted) return NotFound();
        return Ok();
    }
}