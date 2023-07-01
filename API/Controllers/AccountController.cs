using System.Net;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }
    
    [HttpPost("register")]
    public IActionResult Register(RegisterAccountDto register)
    {
        var isCreated = _service.RegisterAccount(register);
        if (!isCreated)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<GetAccountDto> {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<GetAccountDto> {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Register Success"
        });
    }
    
    [HttpPost("Login")]
    public IActionResult Login(LoginAccountDto login)
    {
        var loginResult = _service.LoginAccount(login);
        if (loginResult == "0")
            return NotFound(new ResponseHandler<GetAccountDto> {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found"
            });

        if (loginResult == "-1")
            return BadRequest(new ResponseHandler<GetAccountDto> {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Password is incorrect"
            });

        if (loginResult == "-2")
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<GetAccountDto> {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving when creating token"
            });
        }

        return Ok(new ResponseHandler<string> {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Login Success",
            Data = loginResult
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _service.GetAccount();

        if (entities is null)
        {
            return NotFound(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetAccountDto>>
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
        var entity = _service.GetAccount(guid);
        if (entity is null)
        {
            return NotFound(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<GetAccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entity
        });
    }

    [HttpPost]
    public IActionResult Create(CreateAccountDto newEntityDto)
    {
        var createdEntity = _service.CreateAccount(newEntityDto);
        Console.WriteLine(createdEntity);
        if (createdEntity is null)
        {
            return BadRequest(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<GetAccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createdEntity
        });
    }

    [HttpPut]
    public IActionResult Update(UpdateAccountDto updateEntityDto)
    {
        var update = _service.UpdateAccount(updateEntityDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }

        if (update is 0)
        {
            return BadRequest(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Check your data"
            });
        }

        return Ok(new ResponseHandler<UpdateAccountDto>
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
            return NotFound(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }

        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<GetAccountDto>
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
