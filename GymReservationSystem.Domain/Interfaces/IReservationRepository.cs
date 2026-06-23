using GymReservationSystem.Domain.Entities;

namespace GymReservationSystem.Domain.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Reservation>> GetByTrainingIdAsync(int trainingId);
    }
}
