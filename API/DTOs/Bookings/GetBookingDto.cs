using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Bookings;

public class GetBookingDto
{
    public Guid Guid { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusLevelEnum Status { get; set; }
    public string Remarks { get; set; }
    public Guid RoomGuid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public static implicit operator Booking(GetBookingDto bookingDto)
    {
        return new() {
            Guid = bookingDto.Guid,
            StartDate = bookingDto.StartDate,
            EndDate = bookingDto.EndDate,
            Status = bookingDto.Status,
            Remarks = bookingDto.Remarks,
            RoomGuid = bookingDto.RoomGuid,
            EmployeeGuid = bookingDto.EmployeeGuid
        };
    }
    
    public static explicit operator GetBookingDto(Booking booking)
    {
        return new() {
            Guid = booking.Guid,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status,
            Remarks = booking.Remarks,
            RoomGuid = booking.RoomGuid,
            EmployeeGuid = booking.EmployeeGuid
        };
    }
}
