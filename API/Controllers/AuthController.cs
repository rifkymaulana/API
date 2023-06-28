using System.Net;
using API.DTOs.Auth;
using API.DTOs.Bookings;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult Register(RegisterDto registerDto)
    {
        var createRegister = _authService.Register(registerDto);
        Console.WriteLine("createRegister");
        Console.WriteLine(createRegister);
        if (createRegister is null)
        {
            return BadRequest(new ResponseHandler<GetBookingDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<RegisterDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createRegister
        });
    }
}
