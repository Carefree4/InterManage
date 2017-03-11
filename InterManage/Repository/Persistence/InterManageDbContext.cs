using InterManage.Model;
using Microsoft.EntityFrameworkCore;

namespace InterManage.Repository.Persistence
{
    public class InterManageDbContext : DbContext
    {
        // Employee Stuff
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        // Customer Stuff
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPresence> CustomerPresences { get; set; }
        
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

            modelBuilder.Entity<CustomerPresence>(c =>
            {
                c.HasOne(x => x.Customer);
                c.HasOne(x => x.Employee);
            });
        }
    }
}