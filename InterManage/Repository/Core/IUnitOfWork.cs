using System;

namespace InterManage.Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IShiftRepository Shifts { get; }
        ICustomerPresenceRepository CustomerPresences { get; }
        ICustomerRepository Customers { get; }
        int Commit();
    }
}