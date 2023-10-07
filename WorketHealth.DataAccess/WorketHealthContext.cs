using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess.Models;
using WorketHealth.Domain.Entities;

namespace WorketHealth.DataAccess {
    public class WorketHealthContext : IdentityDbContext{

        public WorketHealthContext(DbContextOptions<WorketHealthContext> options) : base(options)
        {

        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<UserMenuAccess> UserMenuAccess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMenuAccess>()
                .HasKey(uma => new { uma.UserId, uma.MenuId });

            modelBuilder.Entity<UserMenuAccess>()
                .HasOne(uma => uma.User)
                .WithMany(u => u.UserMenuAccess)
                .HasForeignKey(uma => uma.UserId);

            modelBuilder.Entity<UserMenuAccess>()
                .HasOne(uma => uma.Menu)
                .WithMany(m => m.UserMenuAccess)
                .HasForeignKey(uma => uma.MenuId);
        }
    }
}