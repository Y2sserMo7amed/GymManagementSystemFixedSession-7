using GymManagementSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DAL
{
    public class GymDbContext : IdentityDbContext<GymUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.HealthRecord)
                .WithOne(h => h.Member)
                .HasForeignKey<HealthRecord>(h => h.MemberId);

            modelBuilder.Entity<Membership>()
                .HasOne(ms => ms.Member)
                .WithMany(m => m.Memberships)
                .HasForeignKey(ms => ms.MemberId);

            modelBuilder.Entity<Membership>()
                .HasOne(ms => ms.Plan)
                .WithMany(p => p.Memberships)
                .HasForeignKey(ms => ms.PlanId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CategoryId);

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Email).IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Phone).IsUnique();

            modelBuilder.Entity<Trainer>()
                .HasIndex(t => t.Email).IsUnique();

            modelBuilder.Entity<Trainer>()
                .HasIndex(t => t.Phone).IsUnique();

            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = 1, Name = "Basic Plan", Description = "Access to gym equipment during staffed hours", DurationDays = 30, Price = 50, IsActive = true },
                new Plan { Id = 2, Name = "Standard Plan", Description = "Full access to all gym facilities", DurationDays = 60, Price = 100, IsActive = true },
                new Plan { Id = 3, Name = "Premium Plan", Description = "Unlimited access plus personal trainer sessions", DurationDays = 90, Price = 200, IsActive = true }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Boxing" },
                new Category { Id = 2, Name = "Yoga" },
                new Category { Id = 3, Name = "Cardio" },
                new Category { Id = 4, Name = "Weight Training" }
            );
        }
    }
}
