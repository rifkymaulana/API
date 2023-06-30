using API.Models;

namespace API.DTOs.Educations;

public class CreateEducationDto
{
    public Guid Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public double Gpa { get; set; }
    public Guid? UniversityGuid { get; set; }
    
    public static implicit operator Education(CreateEducationDto educationDto)
    {
        return new() {
            Guid = educationDto.Guid,
            Major = educationDto.Major,
            Degree = educationDto.Degree,
            Gpa = educationDto.Gpa,
            UniversityGuid = educationDto.UniversityGuid
        };
    }
    
    public static explicit operator CreateEducationDto(Education education)
    {
        return new() {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            Gpa = education.Gpa,
            UniversityGuid = education.UniversityGuid
        };
    }
}
