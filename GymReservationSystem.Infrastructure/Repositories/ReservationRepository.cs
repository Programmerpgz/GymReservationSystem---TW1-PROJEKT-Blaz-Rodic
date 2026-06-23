using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;
using GymReservationSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymReservationSystem.Infrastructure.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext context) : base(context) 
        {
        
        }

        public async Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId)
        {
            return await _dbSet.Include(r => r.Training).Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByTrainingIdAsync(int trainingId)
        {
            return await _dbSet.Include(r => r.User).Where(r => r.TrainingId == trainingId).ToListAsync();
        }
    }
}
