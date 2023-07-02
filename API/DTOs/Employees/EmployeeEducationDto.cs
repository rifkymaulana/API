using System.ComponentModel.DataAnnotations;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class EmployeeEducationDto
{
    [Required]
    public Guid Guid { get; set; }

    [Required]
    public string Nik { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    [Range(0,1, ErrorMessage = "0 = Female, 1 = Male")]
    public GenderEnum Gender { get; set; }

    [Required]
    public DateTime HiringDate { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    public string Major { get; set; }

    [Required]
    public string Degree { get; set; }

    [Required]
    [Range(0,1, ErrorMessage = "0 = Female, 1 = Male")]
    public double Gpa { get; set; }

    [Required]
    public string UniversityName { get; set; }
}
