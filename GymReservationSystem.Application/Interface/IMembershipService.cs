using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface IMembershipService
    {
        Task<IEnumerable<MembershipDto>> GetAllAsync();
        Task<MembershipDto?> GetByIdAsync(int id);
        Task<MembershipDto> CreateAsync(MembershipDto dto);
        Task<MembershipDto> UpdateAsync(int id, MembershipDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
