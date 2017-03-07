using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InterManage.Model;
using InterManage.Repository.Persistence;

namespace InterManage.ViewModel.EmployeeViewModels
{
    public class EmployeeSchedulesViewModel : ViewModelBase
    {
        private Shift _focusedShift;

        private DateTime selectedDate;

        public EmployeeSchedulesViewModel()
        {
            FocusedShift = DefaultShift();

            RequeryEmployees = new RelayCommand(() => RaisePropertyChanged(nameof(Employees)), () => Employees != null);
            SaveShiftCommand = new RelayCommand(() =>
            {
                SaveShift();
                RaisePropertyChanged(nameof(ShiftsOnSelectedDate));
            }, () => FocusedShift != null);
        }

        public RelayCommand RequeryEmployees { get; set; }
        public RelayCommand SaveShiftCommand { get; set; }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                using (var db = new UnitOfWork(new InterManageDbContext()))
                {
                    return new ObservableCollection<Employee>(db.Employees.GetAll().ToList());
                }
            }
        }

        public ObservableCollection<Shift> ShiftsOnSelectedDate
        {
            get
            {
                using (var db = new UnitOfWork(new InterManageDbContext()))
                {
                    return new ObservableCollection<Shift>(db.Shifts.GetShiftsOnDate(SelectedDate));
                }
            }
        }
        
        public Shift FocusedShift
        {
            get { return _focusedShift; }
            set
            {
                _focusedShift = value;
                RaisePropertyChanged(nameof(FocusedShift));
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                // TODO: Bad bad bad, listen for on changed.
                RaisePropertyChanged(nameof(SelectedDate));
                RaisePropertyChanged(nameof(ShiftsOnSelectedDate));
            }
        }

        private Shift DefaultShift() => new Shift
        {
            Start = DateTime.Now,
            End = DateTime.Now
        };

        public void SaveShift()
        {
            using (var db = new UnitOfWork(new InterManageDbContext()))
            {
                if (FocusedShift.Error == null)
                {
                    var shift = db.Shifts.Get(FocusedShift.Id);
                    if (shift == null)
                    {
                        db.Employees.Get(FocusedShift.AssignedEmployee.Id).Shifts.Add(FocusedShift);
                    }
                    else
                    {
                        shift.AssignedEmployee = FocusedShift.AssignedEmployee;
                        shift.Start = FocusedShift.Start;
                        shift.End = FocusedShift.End;
                    }
                    db.Commit();
                }
            }
            FocusedShift = DefaultShift();
        }

        /*private Shift _focusedShift;
        private DateTime _selectedDate;

        public EmployeeSchedulesViewModel()
        {
            SaveShiftCommand = new RelayCommand(SaveShift, () => FocusedShift != null);

            FocusedShift = new Shift();
            using (var unit = new UnitOfWork(new InterManageDbContext()))
            {
                Employees = unit.Employees.GetAll().ToList();
            }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(nameof(SelectedDate));
            }
        }

        public ObservableCollection<Shift> ShiftsOnSelectedDate { get; set; }

        // TODO: Fix Model.Employee namespace issue thingy
        public List<Model.Employee> Employees { get; }

        public Shift FocusedShift
        {
            get { return _focusedShift; }
            set
            {
                _focusedShift = value;
                RaisePropertyChanged(nameof(FocusedShift));
            }
        }

        public void SaveShift()
        {
            using (var db = new UnitOfWork(new InterManageDbContext()))
            {
                if (FocusedShift.Error == null)
                {
                    var shift = db.Shifts.Get(FocusedShift.Id);
                    if (shift == null)
                    {
                        db.Employees.Get(FocusedShift.AssignedEmployee.Id).Shifts.Add(FocusedShift);
                    }
                    else
                    {
                        shift.AssignedEmployee = FocusedShift.AssignedEmployee;
                        shift.Start = FocusedShift.Start;
                        shift.End = FocusedShift.End;
                    }
                    db.Commit();
                }
            }
        }

        public RelayCommand RemoveShiftCommand { get; set; }
        public RelayCommand SaveShiftCommand { get; set; }
        */
    }
}