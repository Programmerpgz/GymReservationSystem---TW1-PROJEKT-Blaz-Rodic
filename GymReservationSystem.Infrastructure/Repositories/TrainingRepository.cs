using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;
using GymReservationSystem.Infrastructure.Data;

namespace GymReservationSystem.Infrastructure.Repositories
{
    public class TrainingRepository : Repository<Training>, ITrainingRepository
    {
        public TrainingRepository(AppDbContext context) : base(context) 
        { 
        
        }
    }
}
