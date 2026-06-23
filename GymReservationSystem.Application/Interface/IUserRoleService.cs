using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDto>> GetAllAsync();
        Task<UserRoleDto?> GetByIdAsync(int id);
        Task<UserRoleDto> CreateAsync(UserRoleDto dto);
        Task<UserRoleDto> UpdateAsync(int id, UserRoleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
