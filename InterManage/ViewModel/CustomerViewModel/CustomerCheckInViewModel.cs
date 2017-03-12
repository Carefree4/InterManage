using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InterManage.Model;
using InterManage.Repository.Persistence;

namespace InterManage.ViewModel.CustomerViewModel
{
    public class CustomerCheckInViewModel : ViewModelBase
    {
        private CustomerPresence focusedCustomer;

        public CustomerCheckInViewModel()
        {
            UpdateCustomerPresences();
            RequeryEmployees = new RelayCommand(() => RaisePropertyChanged(nameof(Employees)), () => Employees != null);
        }

        public RelayCommand RequeryEmployees { get; set; }

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

        public CustomerPresence FocusedCustomer
        {
            get { return focusedCustomer; }
            set
            {
                focusedCustomer = value;
                RaisePropertyChanged(nameof(FocusedCustomer));
            }
        }

        public ObservableCollection<CustomerPresence> CustomerPresences { get; private set; }

        private void UpdateCustomerPresences()
        {
            using (var db = new UnitOfWork(new InterManageDbContext()))
            {
                CustomerPresences = new ObservableCollection<CustomerPresence>(db.CustomerPresences.GetAll());
            }
        }
    }
}