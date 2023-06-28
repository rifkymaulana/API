using System.Net;
using API.Contracts;
using API.DTOs.Rooms;
using API.Models;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomController : ControllerBase
{
    private readonly RoomService _service;

    public RoomController(IRoomRepository roomRepository)
    {
        _service = new RoomService(roomRepository);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var universities = _service.GetRoom();

        if (universities is null)
        {
            return NotFound(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetRoomDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = universities
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _service.GetRoom(guid);
        if (university is null)
        {
            return NotFound(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<GetRoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = university
        });
    }

    [HttpPost]
    public IActionResult Create(CreateRoomDto newUniversityDto)
    {
        var createdUniversity = _service.CreateRoom(newUniversityDto);
        if (createdUniversity is null)
        {
            return BadRequest(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<GetRoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createdUniversity
        });
    }

    [HttpPut]
    public IActionResult Update(UpdateRoomDto updateUniversityDto)
    {
        var update = _service.UpdateRoom(updateUniversityDto);
        Console.WriteLine(update);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<UpdateRoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }

        if (update is 0)
        {
            return BadRequest(new ResponseHandler<UpdateRoomDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Check your data"
            });
        }

        return Ok(new ResponseHandler<UpdateRoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated",
            Data = updateUniversityDto
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteUniversity(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }

        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<GetRoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }

    [HttpGet("get-by-name/{name}")]
    public IActionResult GetByName(string name)
    {
        var universities = _service.GetRoom(name);
        if (!universities.Any())
        {
            return NotFound(new ResponseHandler<GetRoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No universities found with the given name"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetRoomDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Universities found",
            Data = universities
        });
    }
}