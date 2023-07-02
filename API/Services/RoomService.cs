using API.Contracts;
using API.DTOs.Rooms;
using API.Models;
using API.Utilities.Enums;

namespace API.Services;

public class RoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;

    public RoomService(IRoomRepository roomRepository, IBookingRepository bookingRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
    }

    public IEnumerable<GetRoomDto>? GetRoom()
    {
        var entities = _roomRepository.GetAll();
        if (!entities.Any()) return null;

        var entitiesDtos = entities.Select(u => new GetRoomDto
        {
            Guid = u.Guid,
            Name = u.Name,
            Floor = u.Floor,
            Capacity = u.Capacity
        }).ToList();

        return entitiesDtos;
    }

    public GetRoomDto? GetRoom(Guid guid)
    {
        var entity = _roomRepository.GetByGuid(guid);
        if (entity == null) return null;

        var entityDto = new GetRoomDto
        {
            Guid = entity.Guid,
            Name = entity.Name,
            Floor = entity.Floor,
            Capacity = entity.Capacity
        };

        return entityDto;
    }

    public IEnumerable<GetRoomDto>? GetRoom(string name)
    {
        var entities = _roomRepository.GetByName(name);
        if (entities == null) return null;

        var entityDtos = entities.Select(u => new GetRoomDto
        {
            Guid = u.Guid,
            Name = u.Name,
            Floor = u.Floor,
            Capacity = u.Capacity
        }).ToList();

        return entityDtos;
    }

    public GetRoomDto? CreateRoom(CreateRoomDto createUniversityDto)
    {
        var entity = new Room
        {
            Guid = new Guid(),
            Name = createUniversityDto.Name,
            Floor = createUniversityDto.Floor,
            Capacity = createUniversityDto.Capacity,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEntity = _roomRepository.Create(entity);
        if (createdEntity is null) return null;

        var entityDto = new GetRoomDto
        {
            Guid = createdEntity.Guid,
            Name = createdEntity.Name,
            Floor = createdEntity.Floor,
            Capacity = createdEntity.Capacity
        };

        return entityDto;
    }

    public int UpdateRoom(UpdateRoomDto updateUniversityDto)
    {
        var entity = _roomRepository.IsExist(updateUniversityDto.Guid);
        if (!entity) return -1;

        var entityToUpdate = _roomRepository.GetByGuid(updateUniversityDto.Guid);

        entityToUpdate!.Name = updateUniversityDto.Name;
        entityToUpdate.Floor = updateUniversityDto.Floor;
        entityToUpdate.Capacity = updateUniversityDto.Capacity;
        entityToUpdate.ModifiedDate = DateTime.Now;

        var isUpdated = _roomRepository.Update(entityToUpdate!);
        if (!isUpdated) return 0;

        return 1;
    }

    public int DeleteUniversity(Guid guid)
    {
        var entity = _roomRepository.IsExist(guid);
        if (!entity) return -1;

        var entityToDelete = _roomRepository.GetByGuid(guid);
        var isDeleted = _roomRepository.Delete(entityToDelete!);
        if (!isDeleted) return 0;

        return 1;
    }
    
    public IEnumerable<UnusedRoomDto> GetUnusedRoom()
    {
        var rooms = _roomRepository.GetAll().ToList();

        var usedRooms = from room in _roomRepository.GetAll()
            join booking in _bookingRepository.GetAll()
                on room.Guid equals booking.RoomGuid
            where booking.Status == StatusLevelEnum.OnGoing
            select new UnusedRoomDto
            {
                RoomGuid = room.Guid,
                RoomName = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity
            };
        List<Room> tempRooms = new List<Room>(rooms);

        foreach (var room in rooms)
        {
            foreach (var usedRoom in usedRooms)
            {
                if (room.Guid == usedRoom.RoomGuid)
                {
                    tempRooms.Remove(room); break;
                }
            }
        }

        var unusedRooms = from room in tempRooms
            select new UnusedRoomDto
            {
                RoomGuid = room.Guid,
                RoomName = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity
            };

        return unusedRooms;
    }
}
