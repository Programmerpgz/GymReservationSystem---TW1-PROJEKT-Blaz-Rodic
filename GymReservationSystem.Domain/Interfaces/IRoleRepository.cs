using GymReservationSystem.Domain.Entities;

namespace GymReservationSystem.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name);
    }
}
