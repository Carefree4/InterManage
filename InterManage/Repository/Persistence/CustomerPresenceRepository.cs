using InterManage.Model;
using InterManage.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace InterManage.Repository.Persistence
{
    public class CustomerPresenceRepository : Repository<CustomerPresence>, ICustomerPresenceRepository
    {
        public CustomerPresenceRepository(DbContext context) : base(context)
        {
        }
    }
}