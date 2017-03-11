using InterManage.Repository.Core;

namespace InterManage.Repository.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly InterManageDbContext context;

        public UnitOfWork(InterManageDbContext context)
        {
            this.context = context;
            Employees = new EmployeeRepository(context);
            Shifts = new ShiftRepository(context);
            Customers = new CustomerRepository(context);
            CustomerPresences = new CustomerPresenceRepository(context);
        }

        public IEmployeeRepository Employees { get; }
        public IShiftRepository Shifts { get; }
        public ICustomerPresenceRepository CustomerPresences { get; }
        public ICustomerRepository Customers { get; }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}