using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterManage.Model;
using InterManage.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace InterManage.Repository.Persistence
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }
    }
}
