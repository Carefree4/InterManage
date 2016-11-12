using System;
using System.Data.Entity;

namespace InterManage.Business
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        // I don't know why this fixes the dependency issues... But it does.
        Type providerService = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
    }
}
