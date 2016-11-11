using System.Data.Entity;

namespace InterManage.Business
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
    }
}
