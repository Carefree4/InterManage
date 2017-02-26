using System;
using InterManage.Model;

namespace InterManage.Repository.Core
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee Get(Guid Id);
    }
}
