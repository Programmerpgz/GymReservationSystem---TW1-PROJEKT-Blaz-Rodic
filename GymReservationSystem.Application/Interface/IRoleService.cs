using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto?> GetByIdAsync(int id);
        Task<RoleDto> CreateAsync(RoleDto dto);
        Task<RoleDto> UpdateAsync(int id, RoleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
