using Cybersecurity.Entities;
using Microsoft.EntityFrameworkCore;


namespace Cybersecurity.Data
{
    public class CybersecurityDbContext : DbContext
    {
        public CybersecurityDbContext(DbContextOptions<CybersecurityDbContext> options) : base(options)
        {
            
        }

        public DbSet<Log> Logs { get; set; }
        public DbSet<OldPassword> OldPassword { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<OldPassword>()
                .HasOne(u => u.User)
                .WithMany(o => o.OldPasswords)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin"},
                new Role { Id = 2, Name = "User" },
                new Role { Id = 3, Name = "Block" }
            ); 

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
