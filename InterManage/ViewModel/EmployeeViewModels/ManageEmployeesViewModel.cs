using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InterManage.Repository.Persistence;

namespace InterManage.ViewModel.Employee
{
    internal class ManageEmployeesViewModel : ViewModelBase
    {
        private ObservableCollection<Model.Employee> _employees;


        private Model.Employee _focusedEmployee;

        public ManageEmployeesViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee, () => FocusedEmployee != null);
            RemoveEmployeeCommand = new RelayCommand(RemoveSelectedEmployee, () => FocusedEmployee != null);

            FocusedEmployee = new Model.Employee();
            LoadEmployees();
        }

        public ObservableCollection<Model.Employee> Employees
        {
            get { return _employees; }
            private set
            {
                _employees = value;
                RaisePropertyChanged(nameof(Employees));
            }
        }

        public Model.Employee FocusedEmployee
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

        public RelayCommand AddEmployeeCommand { get; }
        public RelayCommand RemoveEmployeeCommand { get; }

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
            FocusedEmployee = new Model.Employee();
        }

        public void RemoveSelectedEmployee() => RemoveEmployee(FocusedEmployee);

        public void RemoveEmployee(Model.Employee employee)
        {
            using (var unitOfWork = new UnitOfWork(new InterManageDbContext()))
            {
                unitOfWork.Employees.Remove(FocusedEmployee);
                unitOfWork.Commit();
            }
            FocusedEmployee = new Model.Employee();
            LoadEmployees();
        }

        public void LoadEmployees()
        {
            IEnumerable<Model.Employee> employees;
            using (var unitOfWork = new UnitOfWork(new InterManageDbContext()))
            {
                employees = unitOfWork.Employees.GetAll();
            }

            Employees = new ObservableCollection<Model.Employee>(employees);
        }
    }
}