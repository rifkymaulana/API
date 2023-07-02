using API.Utilities.Enums;

namespace API.DTOs.Bookings;

public class GetBookingTodayDto
{
    public Guid BookingGuid { get; set; }
    public string RoomName { get; set; }
    public StatusLevelEnum Status { get; set; }
    public int Floor { get; set; }
    public string BookedBy { get; set; }
}
