using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserDto dto);
        Task<UserDto> UpdateAsync(int id, UserDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
