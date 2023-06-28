using System.Net;
using API.Contracts;
using API.DTOs.AccountRoles;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/account-roles")]
public class AccountRoleController : ControllerBase
{
    private readonly AccountRoleService _service;

    public AccountRoleController(IAccountRoleRepository iRepository)
    {
        _service = new AccountRoleService(iRepository);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _service.GetAccountRole();

        if (entities is null)
        {
            return NotFound(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetAccountRoleDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var entity = _service.GetAccountRole(guid);
        if (entity is null)
        {
            return NotFound(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<GetAccountRoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entity
        });
    }

    [HttpPost]
    public IActionResult Create(CreateAccountRoleDto newEntityDto)
    {
        var createdEntity = _service.CreateAccountRole(newEntityDto);
        Console.WriteLine(createdEntity);
        if (createdEntity is null)
        {
            return BadRequest(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<GetAccountRoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createdEntity
        });
    }

    [HttpPut]
    public IActionResult Update(UpdateAccountRoleDto updateEntityDto)
    {
        var update = _service.UpdateAccountRole(updateEntityDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }

        if (update is 0)
        {
            return BadRequest(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Check your data"
            });
        }

        return Ok(new ResponseHandler<UpdateAccountRoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated",
            Data = updateEntityDto
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteAccountRole(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }

        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<GetAccountRoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }
}
