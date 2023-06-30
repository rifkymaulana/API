using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context)
    {
    }
}