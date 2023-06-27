using System.Net;
using API.Contracts.IServices;
using API.DTOs.Universities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public abstract class BaseController<T> : ControllerBase where T : class
{
    protected readonly IBaseService<T> _service;
    
    public BaseController(IBaseService<T> service)
    {
        _service = service;  
    }
}