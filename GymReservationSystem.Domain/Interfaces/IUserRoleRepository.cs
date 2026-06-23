using GymReservationSystem.Domain.Entities;

namespace GymReservationSystem.Domain.Interfaces
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
        Task<UserRole?> GetByUserAndRoleAsync(int userId, int roleId);
    }
}
