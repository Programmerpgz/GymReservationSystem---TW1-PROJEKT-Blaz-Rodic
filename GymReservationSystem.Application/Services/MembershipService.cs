using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;

namespace GymReservationSystem.Application.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _repository;

    public MembershipService(IMembershipRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MembershipDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToDto);
    }

    public async Task<MembershipDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return null;
        }

        return MapToDto(entity);
    }

    public async Task<MembershipDto> CreateAsync(MembershipDto dto)
    {
        var entity = new Membership
        {
            Name = dto.Name,
            DurationDays = dto.DurationDays,
            Price = dto.Price,
            Description = dto.Description,
            IsActive = dto.IsActive
        };
        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<MembershipDto> UpdateAsync(int id, MembershipDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new Exception("Membership not found");
        }
        entity.Name = dto.Name;
        entity.DurationDays = dto.DurationDays;
        entity.Price = dto.Price;
        entity.Description = dto.Description;
        entity.IsActive = dto.IsActive;
        var updated = await _repository.UpdateAsync(entity);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static MembershipDto MapToDto(Membership m) => new()
    {
        Id = m.Id,
        Name = m.Name,
        DurationDays = m.DurationDays,
        Price = m.Price,
        Description = m.Description,
        IsActive = m.IsActive
    };
}