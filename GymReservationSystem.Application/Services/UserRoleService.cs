using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;

namespace GymReservationSystem.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;

    public UserRoleService(IUserRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserRoleDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToDto);
    }

    public async Task<UserRoleDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return null;
        }

        return MapToDto(entity);
    }

    public async Task<UserRoleDto> CreateAsync(UserRoleDto dto)
    {
        var entity = new UserRole 
        { 
            UserId = dto.UserId, 
            RoleId = dto.RoleId 
        };
        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<UserRoleDto> UpdateAsync(int id, UserRoleDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new Exception("UserRole not found");
        }
        entity.UserId = dto.UserId;
        entity.RoleId = dto.RoleId;
        var updated = await _repository.UpdateAsync(entity);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static UserRoleDto MapToDto(UserRole ur) => new()
    {
        Id = ur.Id,
        UserId = ur.UserId,
        RoleId = ur.RoleId
    };
}