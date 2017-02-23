using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InterManage.Annotations;

namespace InterManage.Model
{
    public class Employee : ModelBase, IDataErrorInfo
    {
        public Guid Id { get; private set; }

        private string firstName;
        private string lastName;

        public Employee()
        {
            Id = Guid.NewGuid();
        }

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

        // TODO: IDataErrorInfo implamentaion
        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "First Name":
                        Error += string.IsNullOrWhiteSpace(FirstName) ? "Name cannot be null or empty. " : null;
                        break;
                    case "Last Name":
                        Error += string.IsNullOrWhiteSpace(FirstName) ? "Name cannot be null or empty. " : null;
                        break;
                }

                return Error;
            }
        }

        public string Error { get; private set; }

        #endregion
    }
}