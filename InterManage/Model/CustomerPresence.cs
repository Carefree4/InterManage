using System;
using System.ComponentModel;

namespace InterManage.Model
{
    public class CustomerPresence : ModelBase, IDataErrorInfo
    {
        private Customer _customer;
        private Employee _employee;
        private DateTime _timeOfCheckIn;

        public CustomerPresence()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }

        public DateTime TimeOfCheckIn
        {
            get { return _timeOfCheckIn; }
            set
            {
                _timeOfCheckIn = value;
                OnPropertyChanged(nameof(TimeOfCheckIn));
            }
        }

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public string Error { get; }

        #endregion
    }
}