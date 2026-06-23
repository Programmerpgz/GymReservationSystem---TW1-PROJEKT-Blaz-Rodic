using GymReservationSystem.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GymReservationSystem.Infrastructure.Data
{
    public static class Seed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.EnsureCreatedAsync();

            if (!context.Users.Any(u => u.Email == "admin@mail.hr"))
            {
                var admin = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@mail.hr",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Phone = "0900000000",
                    IsActive = true
                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();

                context.UserRoles.Add(new UserRole
                {
                    UserId = admin.Id,
                    RoleId = 1
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
