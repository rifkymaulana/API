using API.Contracts;
using API.Contracts.IRepositories;
using API.DTOs.Universities;

namespace API.Services;

public class UniversityService
{
    private readonly IUniversityRepository _universityRepository;
    public UniversityService(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    public IEnumerable<GetUniversityDto> GetAll()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any()) return null!;
        var universityDtos = universities.Select(university => new GetUniversityDto
        {
            Guid = university.Guid, 
            Code = university.Code, 
            Name = university.Name
        });
        return universityDtos;
    }
    
    
}