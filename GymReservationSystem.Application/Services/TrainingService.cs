using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;

namespace GymReservationSystem.Application.Services;

public class TrainingService : ITrainingService
{
    private readonly ITrainingRepository _repository;

    public TrainingService(ITrainingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TrainingDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToDto);
    }

    public async Task<TrainingDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return null;
        }

        return MapToDto(entity);
    }

    public async Task<TrainingDto> CreateAsync(TrainingDto dto)
    {
        var entity = new Training
        {
            Name = dto.Name,
            Description = dto.Description,
            DateTime = dto.DateTime,
            DurationMinutes = dto.DurationMinutes,
            MaxParticipants = dto.MaxParticipants,
            TrainerId = dto.TrainerId,
            RoomId = dto.RoomId,
            IsActive = dto.IsActive
        };
        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<TrainingDto> UpdateAsync(int id, TrainingDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new Exception("Training not found");
        }
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.DateTime = dto.DateTime;
        entity.DurationMinutes = dto.DurationMinutes;
        entity.MaxParticipants = dto.MaxParticipants;
        entity.TrainerId = dto.TrainerId;
        entity.RoomId = dto.RoomId;
        entity.IsActive = dto.IsActive;
        var updated = await _repository.UpdateAsync(entity);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static TrainingDto MapToDto(Training t) => new()
    {
        Id = t.Id,
        Name = t.Name,
        Description = t.Description,
        DateTime = t.DateTime,
        DurationMinutes = t.DurationMinutes,
        MaxParticipants = t.MaxParticipants,
        TrainerId = t.TrainerId,
        RoomId = t.RoomId,
        IsActive = t.IsActive
    };
}