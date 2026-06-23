using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;
using GymReservationSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymReservationSystem.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) 
        {
        
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
