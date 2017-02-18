using InterManage.Model;
using Microsoft.EntityFrameworkCore;

namespace InterManage.Repository.Persistence
{
    public class InterManageDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=InterManage.db");
        }
    }
}