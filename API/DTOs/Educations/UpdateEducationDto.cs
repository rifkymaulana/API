using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Educations;

public class UpdateEducationDto
{
    [Required]
    public Guid Guid { get; set; }
    [Required]
    public string Major { get; set; }
    [Required]
    public string Degree { get; set; }
    [Required]
    public double Gpa { get; set; }
    public Guid? UniversityGuid { get; set; }
}
