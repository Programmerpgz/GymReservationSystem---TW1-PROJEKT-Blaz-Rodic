using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;
using GymReservationSystem.Infrastructure.Data;

namespace GymReservationSystem.Infrastructure.Repositories
{
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        public MembershipRepository(AppDbContext context) : base(context) 
        { 
        
        }
    }
}
