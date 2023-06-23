using API.Contracts;
using API.Data;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : BaseController<Booking>
{
    public BookingController(IBaseRepository<Booking> repository) : base(repository)
    {
    }
}