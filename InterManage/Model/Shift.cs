using System;
using System.ComponentModel;

namespace InterManage.Model
{
    public class Shift : ModelBase, IDataErrorInfo
    {
        private Employee _assignedEmployee;
        private DateTime _start;
        private DateTime _end;

        public Shift()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        // public Guid AssignedEmployeeRefId { get; set; }

        public virtual Employee AssignedEmployee
        {
            get { return _assignedEmployee; }
            set
            {
                _assignedEmployee = value;
                OnPropertyChanged(nameof(AssignedEmployee));
            }
        }

        // TODO: Test this
        public TimeSpan StartTime
        {
            get { return Start.TimeOfDay; }
            set { Start = Start.Date + value; }
        }

        public DateTime StartDay
        {
            get { return Start.Date; }
            set { Start = value + Start.TimeOfDay; }
        }

        public DateTime Start
        {
            get { return _start; }
            set
            {
                _start = value;
                OnPropertyChanged(nameof(Start));
            }
        }

        public TimeSpan EndTime
        {
            get { return End.TimeOfDay; }
            set { End = End.Date + value; }
        }

        public DateTime EndDay
        {
            get { return End.Date; }
            set { End = value + End.TimeOfDay; }
        }

        public DateTime End
        {
            get { return _end; }
            set
            {
                _end = value;
                OnPropertyChanged(nameof(End));
            }
        }
        
        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(AssignedEmployee):
                        Error += AssignedEmployee == null ? "Must assign an employee. " : string.Empty;
                        break;
                }

                return Error;
            }
        }

        public string Error { get; private set; }

        #endregion
    }
}