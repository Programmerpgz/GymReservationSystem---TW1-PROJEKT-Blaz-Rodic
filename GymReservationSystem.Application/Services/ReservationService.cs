using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;

namespace GymReservationSystem.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToDto);
    }

    public async Task<ReservationDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return null;
        }

        return MapToDto(entity);
    }

    public async Task<ReservationDto> CreateAsync(ReservationDto dto)
    {
        var entity = new Reservation
        {
            UserId = dto.UserId,
            TrainingId = dto.TrainingId,
            ReservationDate = dto.ReservationDate,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow
        };
        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<ReservationDto> UpdateAsync(int id, ReservationDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new Exception("Reservation not found");
        }
        entity.UserId = dto.UserId;
        entity.TrainingId = dto.TrainingId;
        entity.ReservationDate = dto.ReservationDate;
        entity.Status = dto.Status;
        var updated = await _repository.UpdateAsync(entity);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ReservationDto>> GetByUserIdAsync(int userId)
    {
        var list = await _repository.GetByUserIdAsync(userId);
        return list.Select(MapToDto);
    }

    private static ReservationDto MapToDto(Reservation r) => new()
    {
        Id = r.Id,
        UserId = r.UserId,
        TrainingId = r.TrainingId,
        ReservationDate = r.ReservationDate,
        Status = r.Status,
        CreatedAt = r.CreatedAt
    };
}