using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;

namespace GymReservationSystem.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return users.Select(MapToDto);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }

        return MapToDto(user);
    }

    public async Task<UserDto> CreateAsync(UserDto dto)
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            IsActive = dto.IsActive,
            MembershipId = dto.MembershipId,
            PasswordHash = ""
        };
        var created = await _repository.CreateAsync(user);
        return MapToDto(created);
    }

    public async Task<UserDto> UpdateAsync(int id, UserDto dto)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.Phone = dto.Phone;
        user.IsActive = dto.IsActive;
        user.MembershipId = dto.MembershipId;
        var updated = await _repository.UpdateAsync(user);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static UserDto MapToDto(User u) => new()
    {
        Id = u.Id,
        FirstName = u.FirstName,
        LastName = u.LastName,
        Email = u.Email,
        Phone = u.Phone,
        IsActive = u.IsActive,
        MembershipId = u.MembershipId
    };
}
