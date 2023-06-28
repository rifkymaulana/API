using API.Contracts;
using API.DTOs.Universities;
using API.Models;

namespace API.Services;

public class UniversityService
{
    private readonly IUniversityRepository _universityRepository;
    public UniversityService(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    public IEnumerable<GetUniversityDto>? GetUniversity()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any()) return null;
        
        var universityDtos = universities.Select(u => new GetUniversityDto
        {
            Guid = u.Guid,
            Name = u.Name,
            Code = u.Code
        }).ToList();

        return universityDtos;
    }
    
    public GetUniversityDto? GetUniversity(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university == null) return null;
        
        var universityDto = new GetUniversityDto
        {
            Guid = university.Guid,
            Name = university.Name,
            Code = university.Code
        };

        return universityDto;
    }
    
    public IEnumerable<GetUniversityDto>? GetUniversity(string name)
    {
        var university = _universityRepository.GetByName(name);
        if (university == null) return null;
        
        var universityDtos = university.Select(u => new GetUniversityDto
        {
            Guid = u.Guid,
            Name = u.Name,
            Code = u.Code
        }).ToList();

        return universityDtos;
    }
    
    public GetUniversityDto? CreateUniversity(CreateUniversityDto createUniversityDto)
    {
        var university = new University
        {
            Guid = new Guid(),
            Code = createUniversityDto.Code,
            Name = createUniversityDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdUniversity = _universityRepository.Create(university);
        if (createdUniversity is null) return null;
        
        var universityDto = new GetUniversityDto
        {
            Guid = createdUniversity.Guid,
            Name = createdUniversity.Name,
            Code = createdUniversity.Code
        };

        return universityDto;
    }
    
    public int UpdateUniversity(UpdateUniversityDto updateUniversityDto)
    {
        var university = _universityRepository.IsExist(updateUniversityDto.Guid);
        if (!university) return -1;
        
        var universityToUpdate = _universityRepository.GetByGuid(updateUniversityDto.Guid);

        universityToUpdate!.Name = updateUniversityDto.Name;
        universityToUpdate.Code = updateUniversityDto.Code;
        universityToUpdate.ModifiedDate = DateTime.Now;

        var isUpdated = _universityRepository.Update(universityToUpdate!);
        if (!isUpdated) return 0;

        return 1;
    }
    
    public int DeleteUniversity(Guid guid)
    {
        var university = _universityRepository.IsExist(guid);
        if (!university) return -1;
        
        var universityToDelete = _universityRepository.GetByGuid(guid);
        var isDeleted = _universityRepository.Delete(universityToDelete!);
        if (!isDeleted) return 0;

        return 1;
    }
}