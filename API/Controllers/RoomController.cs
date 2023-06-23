﻿using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomController : BaseController<Room>
{
    public RoomController(IBaseRepository<Room> repository) : base(repository)
    {
    }
}