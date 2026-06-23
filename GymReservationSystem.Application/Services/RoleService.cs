using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;

namespace GymReservationSystem.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RoleDto>> GetAllAsync()
    {
        var roles = await _repository.GetAllAsync();
        return roles.Select(MapToDto);
    }

    public async Task<RoleDto?> GetByIdAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role == null)
        {
            return null;
        }

        return MapToDto(role);
    }

    public async Task<RoleDto> CreateAsync(RoleDto dto)
    {
        var role = new Role 
        { 
            Name = dto.Name, 
            Description = dto.Description 
        };
        var created = await _repository.CreateAsync(role);
        return MapToDto(created);
    }

    public async Task<RoleDto> UpdateAsync(int id, RoleDto dto)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role == null)
        {
            throw new Exception("Role not found");
        }
        role.Name = dto.Name;
        role.Description = dto.Description;
        var updated = await _repository.UpdateAsync(role);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static RoleDto MapToDto(Role r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Description = r.Description
    };
}
