using API.Utilities.Enums;

namespace API.DTOs.Bookings;

public class CreateBookingDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusLevel Status { get; set; }
    public Guid RoomGuid { get; set; }
    public Guid EmployeeGuid { get; set; }
}