using API.DTOs.Bookings;
using API.Models;

namespace API.Contracts;

public interface IBookingRepository : IBaseRepository<Booking>
{
    IEnumerable<BookingDetailDto> GetBookingDetails();
}
