using API.Contracts;
using API.Contracts.IRepositories;
using API.Contracts.IServices;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomController : BaseController<Room>
{
    public RoomController(IRoomService service) : base(service)
    {
    }
}