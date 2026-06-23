using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;
using GymReservationSystem.Infrastructure.Data;

namespace GymReservationSystem.Infrastructure.Repositories
{
    public class TrainerRepository : Repository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(AppDbContext context) : base(context) 
        { 
        
        }
    }
}
