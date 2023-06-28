using API.Contracts;
using API.DTOs.Bookings;
using API.Models;

namespace API.Services;

public class BookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<GetBookingDto>? GetBooking()
    {
        var entities = _repository.GetAll();
        if (!entities.Any()) return null;

        var entitiesDtos = entities.Select(u => new GetBookingDto
        {
            Guid = u.Guid,
            StartDate = u.StartDate,
            EndDate = u.EndDate,
            Status = u.Status,
            RoomGuid = u.RoomGuid,
            EmployeeGuid = u.EmployeeGuid
        }).ToList();

        return entitiesDtos;
    }

    public GetBookingDto? GetBooking(Guid guid)
    {
        var entity = _repository.GetByGuid(guid);
        if (entity == null) return null;

        var entityDto = new GetBookingDto()
        {
            Guid = entity.Guid,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Status = entity.Status,
            RoomGuid = entity.RoomGuid,
            EmployeeGuid = entity.EmployeeGuid
        };

        return entityDto;
    }

    public GetBookingDto? CreateBooking(CreateBookingDto createEntityDto)
    {
        var entity = new Booking()
        {
            Guid = new Guid(),
            StartDate = createEntityDto.StartDate,
            EndDate = createEntityDto.EndDate,
            Status = createEntityDto.Status,
            RoomGuid = createEntityDto.RoomGuid,
            EmployeeGuid = createEntityDto.EmployeeGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEntity = _repository.Create(entity);
        if (createdEntity is null) return null;

        var entityDto = new GetBookingDto()
        {
            Guid = createdEntity.Guid,
            StartDate = createdEntity.StartDate,
            EndDate = createdEntity.EndDate,
            Status = createdEntity.Status,
            RoomGuid = createdEntity.RoomGuid,
            EmployeeGuid = createdEntity.EmployeeGuid
        };

        return entityDto;
    }

    public int UpdateBooking(UpdateBookingDto updateEntityDto)
    {
        var entity = _repository.IsExist(updateEntityDto.Guid);
        if (!entity) return -1;

        var entityToUpdate = _repository.GetByGuid(updateEntityDto.Guid);
        
        entityToUpdate!.StartDate = updateEntityDto.StartDate;
        entityToUpdate.EndDate = updateEntityDto.EndDate;
        entityToUpdate.Status = updateEntityDto.Status;
        entityToUpdate.RoomGuid = updateEntityDto.RoomGuid;
        entityToUpdate.EmployeeGuid = updateEntityDto.EmployeeGuid;
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
