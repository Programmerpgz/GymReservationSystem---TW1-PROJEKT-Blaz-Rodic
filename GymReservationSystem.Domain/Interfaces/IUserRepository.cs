using GymReservationSystem.Domain.Entities;

namespace GymReservationSystem.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
