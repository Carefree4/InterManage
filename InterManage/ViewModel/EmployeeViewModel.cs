using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InterManage.Model;
using InterManage.Repository.Persistence;

namespace InterManage.ViewModel
{
    internal class EmployeeViewModel : ViewModelBase
    {
        public EmployeeViewModel()
        {
            SelectedEmployee = null;
            AddEmployeeCommand = new RelayCommand(AddEmployee, () => SelectedEmployee == null);
            RemoveEmployeeCommand = new RelayCommand(RemoveSelectedEmployee, () => SelectedEmployee != null);

            if (SelectedEmployee != null)
            {
                SelectedEmployee.PropertyChanged += SelectedEmployeeChanged;
            }

            LoadEmployees();
        }
        
        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            private set
            {
                _employees = value;
                RaisePropertyChanged(nameof(Employees));
            }
        }

        
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged(nameof(SelectedEmployee));
            }
        }

        private void SelectedEmployeeChanged(object sender, PropertyChangedEventArgs e)
        {
            FirstName = SelectedEmployee.FirstName;
            LastName = SelectedEmployee.LastName;
        }

        private string _firstName;
        public string FirstName {
            get { return _firstName;  }
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }

        public RelayCommand AddEmployeeCommand { get; private set; }
        public RelayCommand RemoveEmployeeCommand { get; private set; }

        // Don't know why I need this, but it seems it wont work if I dont have it...
        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    foreach (Employee item in e.OldItems)
                    {
                        RemoveEmployee(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Add:
                    foreach (Employee item in e.NewItems)
                    {
                        // AddEmployee(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddEmployee()
        {
            using (var unitOfWork = new UnitOfWork(new InterManageDbContext()))
            {
                var e = new Employee()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName
                };

                if (e.Error == null)
                {
                    unitOfWork.Employees.Add(e);
                    unitOfWork.Commit();
                }
            }
            FirstName = string.Empty;
            LastName = string.Empty;
            LoadEmployees();
        }

        public void RemoveSelectedEmployee() => RemoveEmployee(SelectedEmployee);

        public void RemoveEmployee(Employee employee)
        {
            using (var unitOfWork = new UnitOfWork(new InterManageDbContext()))
            {
                unitOfWork.Employees.Remove(SelectedEmployee);
                unitOfWork.Commit();
            }
        }

        public void LoadEmployees()
        {
            IEnumerable<Employee> employees;
            using (var unitOfWork = new UnitOfWork(new InterManageDbContext()))
            {
                employees = unitOfWork.Employees.GetAll();
            }

            Employees = new ObservableCollection<Employee>(employees);
        }
    }
}