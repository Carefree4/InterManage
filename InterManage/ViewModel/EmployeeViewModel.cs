using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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
            AddEmployeeCommand = new RelayCommand(AddEmployee, () => FocusedEmployee != null);
            RemoveEmployeeCommand = new RelayCommand(RemoveSelectedEmployee, () => FocusedEmployee != null);

            FocusedEmployee = new Employee();
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
        
        
        private Employee _focusedEmployee;
        public Employee FocusedEmployee
        {
            get { return _focusedEmployee; }
            set
            {
                _focusedEmployee = value;
                RaisePropertyChanged(nameof(FocusedEmployee));

                // Hack?
                AddEmployeeCommand.RaiseCanExecuteChanged();
                RemoveEmployeeCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddEmployeeCommand { get; private set; }
        public RelayCommand RemoveEmployeeCommand { get; private set; }
        
        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LoadEmployees();
        }

        private void AddEmployee()
        {
            using (var unitOfWork = new UnitOfWork(new InterManageDbContext()))
            {
                if (FocusedEmployee.Error == null)
                {
                    var e = unitOfWork.Employees.Get(FocusedEmployee.Id);
                    if (e == null)
                    {
                        unitOfWork.Employees.Add(FocusedEmployee);
                    }
                    else
                    {
                        e.FirstName = FocusedEmployee.FirstName;
                        e.LastName = FocusedEmployee.LastName;
                    }
                    unitOfWork.Commit();
                }
            }
            LoadEmployees();
            FocusedEmployee = new Employee();
        }

        public void RemoveSelectedEmployee() => RemoveEmployee(FocusedEmployee);

        public void RemoveEmployee(Employee employee)
        {
            using (var unitOfWork = new UnitOfWork(new InterManageDbContext()))
            {
                unitOfWork.Employees.Remove(FocusedEmployee);
                unitOfWork.Commit();
            }
            LoadEmployees();
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