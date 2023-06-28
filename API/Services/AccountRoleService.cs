using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;

namespace API.Services;

public class AccountRoleService
{
    private readonly IAccountRoleRepository _repository;

    public AccountRoleService(IAccountRoleRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<GetAccountRoleDto>? GetAccountRole()
    {
        var entities = _repository.GetAll();
        if (!entities.Any()) return null;

        var entitiesDtos = entities.Select(u => new GetAccountRoleDto
        {
            Guid = u.Guid,
            AccountGuid = u.AccountGuid,
            RoleGuid = u.RoleGuid
        }).ToList();

        return entitiesDtos;
    }

    public GetAccountRoleDto? GetAccountRole(Guid guid)
    {
        var entity = _repository.GetByGuid(guid);
        if (entity == null) return null;

        var entityDto = new GetAccountRoleDto()
        {
            Guid = entity.Guid,
            AccountGuid = entity.AccountGuid,
            RoleGuid = entity.RoleGuid
        };

        return entityDto;
    }

    public GetAccountRoleDto? CreateAccountRole(CreateAccountRoleDto createEntityDto)
    {
        var entity = new AccountRole()
        {
            Guid = new Guid(),
            AccountGuid = createEntityDto.AccountGuid,
            RoleGuid = createEntityDto.RoleGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEntity = _repository.Create(entity);
        if (createdEntity is null) return null;

        var entityDto = new GetAccountRoleDto()
        {
            Guid = createdEntity.Guid,
            AccountGuid = createdEntity.AccountGuid,
            RoleGuid = createdEntity.RoleGuid
        };

        return entityDto;
    }

    public int UpdateAccountRole(UpdateAccountRoleDto updateEntityDto)
    {
        var entity = _repository.IsExist(updateEntityDto.Guid);
        if (!entity) return -1;

        var entityToUpdate = _repository.GetByGuid(updateEntityDto.Guid);

        entityToUpdate!.AccountGuid = updateEntityDto.AccountGuid;
        entityToUpdate.RoleGuid = updateEntityDto.RoleGuid;
        entityToUpdate.ModifiedDate = DateTime.Now;

        var isUpdated = _repository.Update(entityToUpdate!);
        if (!isUpdated) return 0;

        return 1;
    }

    public int DeleteAccountRole(Guid guid)
    {
        var entity = _repository.IsExist(guid);
        if (!entity) return -1;

        var entityToDelete = _repository.GetByGuid(guid);
        var isDeleted = _repository.Delete(entityToDelete!);
        if (!isDeleted) return 0;

        return 1;
    }
}
