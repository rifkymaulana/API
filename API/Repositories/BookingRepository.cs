using API.Contracts;
using API.Data;
using API.DTOs.Bookings;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(ApplicationDbContext context) : base(context)
    {
    }
    

    public IEnumerable<BookingDetailDto> GetBookingDetails()
    {
        var booking = _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.Employee)
            .Where(b => b.Room.Guid == b.Room.Guid && b.Employee.Guid == b.Employee.Guid)
            .ToList();

        var bookingDetails = booking.Select(b => new BookingDetailDto
        {
            Guid = b.Guid,
            BookedNik = b.Employee.Nik,
            BookedBy = b.Employee.FirstName + " " + b.Employee.LastName,
            RoomName = b.Room.Name,
            StartDate = b.StartDate,
            EndDate = b.EndDate,
            Status = b.Status,
            Remarks = b.Remarks
        });
        return bookingDetails;
    }
}
