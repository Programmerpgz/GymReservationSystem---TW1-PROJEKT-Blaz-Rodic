using Microsoft.EntityFrameworkCore;
using GymReservationSystem.Domain.Entities;

namespace GymReservationSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Training> Trainings => Set<Training>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Membership> Memberships => Set<Membership>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>()
            .HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Trainer>()
            .HasIndex(t => t.Email).IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Membership)
            .WithMany(m => m.Users)
            .HasForeignKey(u => u.MembershipId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Training>()
            .HasOne(t => t.Trainer)
            .WithMany(tr => tr.Trainings)
            .HasForeignKey(t => t.TrainerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Training>()
            .HasOne(t => t.Room)
            .WithMany(r => r.Trainings)
            .HasForeignKey(t => t.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Training)
            .WithMany(t => t.Reservations)
            .HasForeignKey(r => r.TrainingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", Description = "Administrator sustava" },
            new Role { Id = 2, Name = "User", Description = "Obicni korisnik" }
        );

        modelBuilder.Entity<Membership>().HasData(
            new Membership { Id = 1, Name = "Mjesecna", DurationDays = 30, Price = 50.00m, Description = "Mjesecna clanarina", IsActive = true },
            new Membership { Id = 2, Name = "Godisnja", DurationDays = 365, Price = 400.00m, Description = "Godisnja clanarina", IsActive = true }
        );

        modelBuilder.Entity<Trainer>().HasData(
            new Trainer { Id = 1, FirstName = "Marko", LastName = "Markovic", Specialization = "Fitness", Phone = "0911111111", Email = "marko@gym.com", IsActive = true },
            new Trainer { Id = 2, FirstName = "Ana", LastName = "Anic", Specialization = "Yoga", Phone = "0922222222", Email = "ana@gym.com", IsActive = true }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Name = "Sala A", Capacity = 20, Description = "Velika sala za grupne treninge", IsActive = true },
            new Room { Id = 2, Name = "Sala B", Capacity = 10, Description = "Mala sala za jogu", IsActive = true }
        );
    }
}