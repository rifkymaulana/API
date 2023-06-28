using API.Contracts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.Models;

namespace API.Services;

public class EducationService
{
    private readonly IEducationRepository _repository;

    public EducationService(IEducationRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<GetEducationDto>? GetEducation()
    {
        var entities = _repository.GetAll();
        if (!entities.Any()) return null;

        var entitiesDtos = entities.Select(u => new GetEducationDto
        {
            Guid = u.Guid,
            Major = u.Major,
            Degree = u.Degree,
            Gpa = u.Gpa,
            UniversityGuid = u.UniversityGuid,
        }).ToList();

        return entitiesDtos;
    }

    public GetEducationDto? GetEducation(Guid guid)
    {
        var entity = _repository.GetByGuid(guid);
        if (entity == null) return null;

        var entityDto = new GetEducationDto()
        {
            Guid = entity.Guid,
            Major = entity.Major,
            Degree = entity.Degree,
            Gpa = entity.Gpa,
            UniversityGuid = entity.UniversityGuid,
        };

        return entityDto;
    }

    public IEnumerable<GetEducationDto>? GetEducation(string name)
    {
        var entities = _repository.GetByName(name);
        if (entities == null) return null;

        var entityDtos = entities.Select(u => new GetEducationDto
        {
            Guid = u.Guid,  
            Major = u.Major,
            Degree = u.Degree,
            Gpa = u.Gpa,
            UniversityGuid = u.UniversityGuid,
        }).ToList();

        return entityDtos;
    }

    public GetEducationDto? CreateEducation(CreateEducationDto createEntityDto)
    {
        var entity = new Education()
        {
            Guid = createEntityDto.Guid,
            Major = createEntityDto.Major,
            Degree = createEntityDto.Degree,
            Gpa = createEntityDto.Gpa,
            UniversityGuid = createEntityDto.UniversityGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEntity = _repository.Create(entity);
        if (createdEntity is null) return null;

        var entityDto = new GetEducationDto()
        {
            Guid = createdEntity.Guid,
            Major = createdEntity.Major,
            Degree = createdEntity.Degree,
            Gpa = createdEntity.Gpa,
            UniversityGuid = createdEntity.UniversityGuid,
        };

        return entityDto;
    }

    public int UpdateEducation(UpdateEducationDto updateEntityDto)
    {
        var entity = _repository.IsExist(updateEntityDto.Guid);
        if (!entity) return -1;

        var entityToUpdate = _repository.GetByGuid(updateEntityDto.Guid);

        entityToUpdate!.Major = updateEntityDto.Major;
        entityToUpdate.Degree = updateEntityDto.Degree;
        entityToUpdate.Gpa = updateEntityDto.Gpa;
        entityToUpdate.UniversityGuid = updateEntityDto.UniversityGuid;
        entityToUpdate.ModifiedDate = DateTime.Now;

        var isUpdated = _repository.Update(entityToUpdate!);
        if (!isUpdated) return 0;

        return 1;
    }

    public int DeleteEducation(Guid guid)
    {
        var entity = _repository.IsExist(guid);
        if (!entity) return -1;

        var entityToDelete = _repository.GetByGuid(guid);
        var isDeleted = _repository.Delete(entityToDelete!);
        if (!isDeleted) return 0;

        return 1;
    }
}
