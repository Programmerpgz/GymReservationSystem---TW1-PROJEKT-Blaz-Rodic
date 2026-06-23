using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;

namespace GymReservationSystem.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToDto);
    }

    public async Task<RoomDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return null;
        }

        return MapToDto(entity);
    }

    public async Task<RoomDto> CreateAsync(RoomDto dto)
    {
        var entity = new Room
        {
            Name = dto.Name,
            Capacity = dto.Capacity,
            Description = dto.Description,
            IsActive = dto.IsActive
        };
        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<RoomDto> UpdateAsync(int id, RoomDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new Exception("Room not found");
        }
        entity.Name = dto.Name;
        entity.Capacity = dto.Capacity;
        entity.Description = dto.Description;
        entity.IsActive = dto.IsActive;
        var updated = await _repository.UpdateAsync(entity);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static RoomDto MapToDto(Room r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Capacity = r.Capacity,
        Description = r.Description,
        IsActive = r.IsActive
    };
}