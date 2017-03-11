using System;
using System.ComponentModel;

namespace InterManage.Model
{
    public class Customer : ModelBase, IDataErrorInfo
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;

        public Customer()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public string Error { get; }

        #endregion
    }
}