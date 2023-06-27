using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public abstract class BaseController<T> : ControllerBase where T : class
{
    protected readonly IBaseRepository<T> _repository;
    
    public BaseController(IBaseRepository<T> repository)
    {
        _repository = repository;  
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _repository.GetAll();
        if (!entities.Any()) return NotFound();
        return Ok(entities);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var entity = _repository.GetByGuid(guid);
        if (entity is null) return NotFound();
        return Ok(entity);
    }
    
    [HttpPost]
    public IActionResult Create(T entity)
    {
        var isCreated = _repository.Create(entity);
        return Ok(isCreated);
    }
    
    [HttpPut]
    public IActionResult Update(T entity)
    {
        var isUpdated = _repository.Update(entity);
        if (!isUpdated) return NotFound();
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete(T entity)
    {
        var isDeleted = _repository.Delete(entity);
        if (!isDeleted) return NotFound();
        return Ok();
    }
}