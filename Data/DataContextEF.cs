using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;
        
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<User> Users { get; set;}
        public virtual DbSet<UserSalary> UserSalary { get; set;}
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                       optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.HasDefaultSchema ("TutorialAppSchema");

            modelBuilder.Entity<User>()
                .ToTable ("Users","TutorialAppSchema")
                .HasKey(u => u.UserId);
                modelBuilder.HasDefaultSchema ("TutorialAppSchema");

            modelBuilder.Entity<UserSalary>()
                .HasKey(u => u.UserId);
                modelBuilder.HasDefaultSchema ("TutorialAppSchema");

            modelBuilder.Entity<UserJobInfo>()
               .HasKey(u => u.UserId);
        }
    }
}
