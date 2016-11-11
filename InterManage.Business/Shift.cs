using System;

namespace InterManage.Business
{
    public class Shift
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
