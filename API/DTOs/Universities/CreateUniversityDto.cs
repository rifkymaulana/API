using API.Models;

namespace API.DTOs.Universities;

public class CreateUniversityDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    
    public static implicit operator University(CreateUniversityDto newUniversityDto)
    {
        return new() {
            Guid = new Guid(),
            Code = newUniversityDto.Code,
            Name = newUniversityDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
    
    public static explicit operator CreateUniversityDto(University university)
    {
        return new() {
            Code = university.Code,
            Name = university.Name
        };
    }
}
