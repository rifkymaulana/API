using System.ComponentModel.DataAnnotations;
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Bookings;

public class CreateBookingDto
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public string Remarks { get; set; }
    [Required]
    public Guid RoomGuid { get; set; }
    [Required]
    public Guid EmployeeGuid { get; set; }
    public static implicit operator Booking(CreateBookingDto newBookingDto)
    {
        return new() {
            Guid = new Guid(),
            StartDate = newBookingDto.StartDate,
            EndDate = newBookingDto.EndDate,
            Status = StatusLevelEnum.Requested,
            Remarks = newBookingDto.Remarks,
            RoomGuid = newBookingDto.RoomGuid,
            EmployeeGuid = newBookingDto.EmployeeGuid
        };
    }
    
    public static explicit operator CreateBookingDto(Booking booking)
    {
        return new() {
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Remarks = booking.Remarks,
            RoomGuid = booking.RoomGuid,
            EmployeeGuid = booking.EmployeeGuid
        };
    }
}
