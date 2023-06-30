using API.Contracts;
using API.DTOs.Roles;
using API.Models;

namespace API.Services;

public class RoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<GetRoleDto>? GetRole()
    {
        var entities = _repository.GetAll();
        if (!entities.Any()) return null;

        var entitiesDtos = entities.Select(u => new GetRoleDto
        {
            Guid = u.Guid,
            Name = u.Name,
        }).ToList();

        return entitiesDtos;
    }

    public GetRoleDto? GetRole(Guid guid)
    {
        var entity = _repository.GetByGuid(guid);
        if (entity == null) return null;

        var entityDto = new GetRoleDto()
        {
            Guid = entity.Guid,
            Name = entity.Name,
        };

        return entityDto;
    }

    public GetRoleDto? GetRole(string name)
    {
        var entities = _repository.GetByName(name);
        if (entities == null) return null;

        var entityDtos = new GetRoleDto()
        {
            Guid = entities.Guid,
            Name = entities.Name
        };

        return entityDtos;
    }

    public GetRoleDto? CreateRole(CreateRoleDto createEntityDto)
    {
        var entity = new Role
        {
            Guid = new Guid(),
            Name = createEntityDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEntity = _repository.Create(entity);
        if (createdEntity is null) return null;

        var entityDto = new GetRoleDto()
        {
            Guid = createdEntity.Guid,
            Name = createdEntity.Name,
        };

        return entityDto;
    }

    public int UpdateRole(UpdateRoleDto updateEntityDto)
    {
        var entity = _repository.IsExist(updateEntityDto.Guid);
        if (!entity) return -1;

        var entityToUpdate = _repository.GetByGuid(updateEntityDto.Guid);

        entityToUpdate!.Name = updateEntityDto.Name;
        entityToUpdate.ModifiedDate = DateTime.Now;

        var isUpdated = _repository.Update(entityToUpdate!);
        if (!isUpdated) return 0;

        return 1;
    }

    public int DeleteUniversity(Guid guid)
    {
        var entity = _repository.IsExist(guid);
        if (!entity) return -1;

        var entityToDelete = _repository.GetByGuid(guid);
        var isDeleted = _repository.Delete(entityToDelete!);
        if (!isDeleted) return 0;

        return 1;
    }
}
