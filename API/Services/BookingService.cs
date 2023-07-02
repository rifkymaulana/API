using API.Contracts;
using API.DTOs.Bookings;
using API.Models;

namespace API.Services;

public class BookingService
{
    private readonly IBookingRepository _repository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoomRepository _roomRepository;

    public BookingService(IBookingRepository repository, IEmployeeRepository employeeRepository,
        IRoomRepository roomRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
        _roomRepository = roomRepository;
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
    
    public List<BookingDetailDto>? GetBookingDetails()
    {
        var booking = _repository.GetBookingDetails();
        var bookingDetails = booking.Select(b => new BookingDetailDto
        {
            Guid = b.Guid,
            BookedNik = b.BookedNik,
            BookedBy = b.BookedBy,
            RoomName = b.RoomName,
            StartDate = b.StartDate,
            EndDate = b.EndDate,
            Status = b.Status,
            Remarks = b.Remarks
        }).ToList();

        return bookingDetails;
    }

    public BookingDetailDto? GetBookingDetailsByGuid(Guid guid)
    {
        var relatedBooking = GetBookingDetails().SingleOrDefault(b => b.Guid == guid);
        return relatedBooking;
    }
    
    
    public IEnumerable<GetBookingTodayDto>? BookingToday()
    {
        var bookings = _repository.GetAll();
        if (bookings is null)
        {
            return null;
        }

        var employees = _employeeRepository.GetAll();
        var rooms = _roomRepository.GetAll();

        var bookingToday = (from booking in bookings
            join employee in employees on booking.EmployeeGuid equals employee.Guid
            join room in rooms on booking.RoomGuid equals room.Guid
            where booking.StartDate <= DateTime.Now.Date && booking.EndDate >= DateTime.Now
            select new GetBookingTodayDto
            {
                BookingGuid = booking.Guid,
                RoomName = room.Name,
                Status = booking.Status,
                Floor = room.Floor,
                BookedBy = employee.FirstName + " " + employee.LastName
            }).ToList();

        if (!bookingToday.Any())
        {
            return null;
        }

        return bookingToday;
    }
    
    public IEnumerable<BookingLengthDto>? BookingDuration()
    {
        var bookings = _repository.GetAll();
        var rooms = _roomRepository.GetAll();

        var entities = (from booking in bookings
            join room in rooms on booking.RoomGuid equals room.Guid
            select new
            {
                guid = room.Guid,
                startDate = booking.StartDate,
                endDate = booking.EndDate,
                roomName = room.Name
            }).ToList();

        var bookingDurations = new List<BookingLengthDto>();

        foreach (var entity in entities)
        {
            TimeSpan duration = entity.endDate - entity.startDate;

            // Count the number of weekends within the duration
            int totalDays = (int)duration.TotalDays;
            int weekends = 0;

            for (int i = 0; i <= totalDays; i++)
            {
                var currentDate = entity.startDate.AddDays(i);
                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekends++;
                }
            }

            // Calculate the duration without weekends
            TimeSpan bookingLength = duration - TimeSpan.FromDays(weekends);

            var bookingDurationDto = new BookingLengthDto
            {
                RoomGuid = entity.guid,
                RoomName = entity.roomName,
                BookingLength = bookingLength
            };

            bookingDurations.Add(bookingDurationDto);
        }

        return bookingDurations;
    }
}
