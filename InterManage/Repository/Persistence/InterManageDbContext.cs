using InterManage.Model;
using Microsoft.EntityFrameworkCore;

namespace InterManage.Repository.Persistence
{
    public class InterManageDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./InterManage.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(e =>
            {
                e.HasMany(x => x.Shifts)
                    .WithOne(s => s.AssignedEmployee)
                    .HasConstraintName("ForeignKey_Employee_Shift");
            });
        }
    }
}