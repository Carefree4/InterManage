using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterManage.Model;

namespace InterManage.Repository.Core
{
    public interface IShiftRepository : IRepository<Shift>
    {
        Shift Get(Guid Id);
        IEnumerable<Shift> GetShiftsOnDate(DateTime day);
    }
}
