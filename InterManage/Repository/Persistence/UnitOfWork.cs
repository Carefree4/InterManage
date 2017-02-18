using InterManage.Repository.Core;

namespace InterManage.Repository.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository Employees { get; }

        private readonly InterManageDbContext context;
        
        public UnitOfWork(InterManageDbContext context)
        {
            this.context = context;
            Employees = new EmployeeRepository(context);
        }

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