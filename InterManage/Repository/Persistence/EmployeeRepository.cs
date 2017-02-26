using System;
using System.Linq;
using InterManage.Model;
using InterManage.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace InterManage.Repository.Persistence
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context)
        {
        }

        public Employee Get(Guid id) => Context.Set<Employee>().Find(id);
    }
}