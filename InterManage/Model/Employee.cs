using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InterManage.Model
{
    public class Employee : ModelBase, IDataErrorInfo
    {
        private string firstName;
        private string lastName;

        public Employee()
        {
            Id = Guid.NewGuid();
            Shifts = new List<Shift>();
        }

        public Guid Id { get; private set; }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public virtual ICollection<Shift> Shifts { get; set; }

        public string FullName => FirstName + " " + LastName;

        public override string ToString() => FullName;

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        Error += string.IsNullOrWhiteSpace(FirstName)
                            ? "First name cannot be null or empty. "
                            : string.Empty;
                        break;
                    case nameof(lastName):
                        Error += string.IsNullOrWhiteSpace(FirstName)
                            ? "Last name cannot be null or empty. "
                            : string.Empty;
                        break;
                }

                return Error;
            }
        }

        public string Error { get; private set; }

        #endregion
    }
}