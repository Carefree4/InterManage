using System;
using System.Collections.Generic;
using System.Linq;
using InterManage.Model;
using InterManage.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace InterManage.Repository.Persistence
{
    public class ShiftRepository : Repository<Shift>, IShiftRepository
    {
        public ShiftRepository(DbContext context) : base(context)
        {
        }

        public Shift Get(Guid id) => Context.Set<Shift>().Find(id);

        public IEnumerable<Shift> GetShiftsOnDate(DateTime day)
            => Context.Set<Shift>().Where(s => s.Start.Date.Equals(day.Date) || s.End.Date.Equals(day.Date));
    }
}