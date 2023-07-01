using API.Contracts;
using API.DTOs.Employees;
using API.Models;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<GetEmployeeDto>? GetEmployee()
    {
        var entities = _repository.GetAll();
        if (!entities.Any()) return null;

        var entitiesDtos = entities.Select(u => new GetEmployeeDto
        {
            Guid = u.Guid,
            Nik = u.Nik,
            FirstName = u.FirstName,
            LastName = u.LastName,
            BirthDate = u.BirthDate,
            Gender = u.Gender,
            HiringDate = u.HiringDate,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
        }).ToList();

        return entitiesDtos;
    }

    public GetEmployeeDto? GetEmployee(Guid guid)
    {
        var entity = _repository.GetByGuid(guid);
        if (entity == null) return null;

        var entityDto = new GetEmployeeDto()
        {
            Guid = entity.Guid,
            Nik = entity.Nik,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BirthDate = entity.BirthDate,
            Gender = entity.Gender,
            HiringDate = entity.HiringDate,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
        };

        return entityDto;
    }

    public IEnumerable<GetEmployeeDto>? GetEmployee(string name)
    {
        var entities = _repository.GetByName(name);
        if (entities == null) return null;

        var entityDtos = entities.Select(u => new GetEmployeeDto
        {
            Guid = u.Guid,
            Nik = u.Nik,
            FirstName = u.FirstName,
            LastName = u.LastName,
            BirthDate = u.BirthDate,
            Gender = u.Gender,
            HiringDate = u.HiringDate,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
        }).ToList();

        return entityDtos;
    }

    public GetEmployeeDto GetEmployeeByEmail(string email)
    {
        var entity = _repository.GetByEmail(email);
        if (entity == null) return null;

        var entityDto = new GetEmployeeDto()
        {
            Guid = entity.Guid,
            Nik = entity.Nik,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BirthDate = entity.BirthDate,
            Gender = entity.Gender,
            HiringDate = entity.HiringDate,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
        };

        return entityDto;
    }

    public GetEmployeeDto? CreateEmployee(CreateEmployeeDto createEntityDto)
    {
        Console.WriteLine(createEntityDto.BirthDate);
        Console.WriteLine(createEntityDto.HiringDate.GetType());
        var entity = new Employee
        {
            Guid = new Guid(),
            Nik = createEntityDto.Nik,
            FirstName = createEntityDto.FirstName,
            LastName = createEntityDto.LastName,
            BirthDate = createEntityDto.BirthDate,
            Gender = createEntityDto.Gender,
            HiringDate = createEntityDto.HiringDate,
            Email = createEntityDto.Email,
            PhoneNumber = createEntityDto.PhoneNumber,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEntity = _repository.Create(entity);
        if (createdEntity is null) return null;

        var entityDto = new GetEmployeeDto()
        {
            Guid = createdEntity.Guid,
            Nik = createEntityDto.Nik,
            FirstName = createdEntity.FirstName,
            LastName = createdEntity.LastName,
            BirthDate = createdEntity.BirthDate,
            Gender = createEntityDto.Gender,
            HiringDate = createEntityDto.HiringDate,
            Email = createEntityDto.Email,
            PhoneNumber = createEntityDto.PhoneNumber,
        };

        return entityDto;
    }

    public int UpdateEmployee(UpdateEmployeeDto updateEntityDto)
    {
        var entity = _repository.IsExist(updateEntityDto.Guid);
        if (!entity) return -1;

        var entityToUpdate = _repository.GetByGuid(updateEntityDto.Guid);
    
        entityToUpdate!.Nik = updateEntityDto.Nik;
        entityToUpdate!.FirstName = updateEntityDto.FirstName;
        entityToUpdate.LastName = updateEntityDto.LastName;
        entityToUpdate.BirthDate = updateEntityDto.BirthDate;
        entityToUpdate.Gender = updateEntityDto.Gender;
        entityToUpdate.HiringDate = updateEntityDto.HiringDate;
        entityToUpdate.Email = updateEntityDto.Email;
        entityToUpdate.PhoneNumber = updateEntityDto.PhoneNumber;
        entityToUpdate.ModifiedDate = DateTime.Now;

        var isUpdated = _repository.Update(entityToUpdate!);
        if (!isUpdated) return 0;

        return 1;
    }

    public int DeleteEmployee(Guid guid)
    {
        var entity = _repository.IsExist(guid);
        if (!entity) return -1;

        var entityToDelete = _repository.GetByGuid(guid);
        var isDeleted = _repository.Delete(entityToDelete!);
        if (!isDeleted) return 0;

        return 1;
    }
}
