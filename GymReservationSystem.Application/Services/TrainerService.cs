using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;

namespace GymReservationSystem.Application.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _repository;

    public TrainerService(ITrainerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TrainerDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToDto);
    }

    public async Task<TrainerDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return null;
        }

        return MapToDto(entity);
    }

    public async Task<TrainerDto> CreateAsync(TrainerDto dto)
    {
        var entity = new Trainer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Specialization = dto.Specialization,
            Phone = dto.Phone,
            Email = dto.Email,
            IsActive = dto.IsActive
        };
        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<TrainerDto> UpdateAsync(int id, TrainerDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new Exception("Trainer not found");
        }
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Specialization = dto.Specialization;
        entity.Phone = dto.Phone;
        entity.Email = dto.Email;
        entity.IsActive = dto.IsActive;
        var updated = await _repository.UpdateAsync(entity);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static TrainerDto MapToDto(Trainer t) => new()
    {
        Id = t.Id,
        FirstName = t.FirstName,
        LastName = t.LastName,
        Specialization = t.Specialization,
        Phone = t.Phone,
        Email = t.Email,
        IsActive = t.IsActive
    };
}