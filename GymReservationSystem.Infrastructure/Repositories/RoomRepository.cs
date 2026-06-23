using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;
using GymReservationSystem.Infrastructure.Data;

namespace GymReservationSystem.Infrastructure.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context) 
        {
        
        }
    }
}
