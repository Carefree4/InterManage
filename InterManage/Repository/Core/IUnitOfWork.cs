using System;

namespace InterManage.Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IShiftRepository Shifts { get; }
        int Commit();
    }
}