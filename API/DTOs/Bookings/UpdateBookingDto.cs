using System.ComponentModel.DataAnnotations;
using API.Utilities.Enums;

namespace API.DTOs.Bookings;

public class UpdateBookingDto
{
    [Required]
    public Guid Guid { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public StatusLevelEnum Status { get; set; }
    [Required]
    public Guid RoomGuid { get; set; }
    [Required]
    public Guid EmployeeGuid { get; set; }
}
