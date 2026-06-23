using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;
using GymReservationSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymReservationSystem.Infrastructure.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context) : base(context) 
        { 
        
        }

        public async Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId)
        {
            return await _dbSet.Include(ur => ur.Role).Where(ur => ur.UserId == userId).ToListAsync();
        }

        public async Task<UserRole?> GetByUserAndRoleAsync(int userId, int roleId)
        {
            return await _dbSet.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
