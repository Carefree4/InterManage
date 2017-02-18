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
    }
}