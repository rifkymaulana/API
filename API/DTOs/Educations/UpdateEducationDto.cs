namespace API.DTOs.Educations;

public class UpdateEducationDto
{
    public Guid Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public double GPA { get; set; }
    public Guid UniversityGuid { get; set; }
}