using API.Contracts;
using API.DTOs.Accounts;
using API.Models;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<GetAccountDto>? GetAccount()
    {
        var entities = _repository.GetAll();
        if (!entities.Any()) return null;

        var entitiesDtos = entities.Select(u => new GetAccountDto
        {
            EmployeeGuid = u.Guid,
            Password = u.Password,
            IsDeleted = u.IsDeleted
            
        }).ToList();

        return entitiesDtos;
    }

    public GetAccountDto? GetAccount(Guid guid)
    {
        var entity = _repository.GetByGuid(guid);
        if (entity == null) return null;

        var entityDto = new GetAccountDto()
        {
            EmployeeGuid = entity.Guid,
            Password = entity.Password,
            IsDeleted = entity.IsDeleted
        };

        return entityDto;
    }

    public GetAccountDto? CreateAccount(CreateAccountDto createEntityDto)
    {
        var entity = new Account()
        {
            Guid = createEntityDto.EmployeeGuid,
            Password = createEntityDto.Password,
            IsDeleted = createEntityDto.IsDeleted,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEntity = _repository.Create(entity);
        if (createdEntity is null) return null;

        var entityDto = new GetAccountDto()
        {
            EmployeeGuid = createdEntity.Guid,
            Password = createdEntity.Password,
            IsDeleted = createdEntity.IsDeleted
        };

        return entityDto;
    }

    public int UpdateAccount(UpdateAccountDto updateEntityDto)
    {
        var entity = _repository.IsExist(updateEntityDto.EmployeeGuid);
        if (!entity) return -1;

        var entityToUpdate = _repository.GetByGuid(updateEntityDto.EmployeeGuid);

        entityToUpdate!.Password = updateEntityDto.Password;
        entityToUpdate.IsDeleted = updateEntityDto.IsDeleted;
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
