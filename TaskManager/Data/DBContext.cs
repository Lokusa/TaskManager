using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class DBContext : IdentityDbContext<User>
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        // Explicitly use TaskManager.Models.Task to resolve ambiguity
        public DbSet<TaskManager.Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskManager.Models.Task>()
                .HasKey(t => t.TaskId);

            modelBuilder.Entity<TaskManager.Models.Task>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
