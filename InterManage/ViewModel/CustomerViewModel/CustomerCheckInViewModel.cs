using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using InterManage.Model;
using InterManage.Repository.Persistence;

namespace InterManage.ViewModel.CustomerViewModel
{
    public class CustomerCheckInViewModel : ViewModelBase
    {

        private CustomerPresence focusedCustomer;

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

        public CustomerCheckInViewModel()
        {
            UpdateCustomerPresences();
        }

        private void UpdateCustomerPresences()
        {
            using (var db = new UnitOfWork(new InterManageDbContext()))
            {
                CustomerPresences = new ObservableCollection<CustomerPresence>(db.CustomerPresences.GetAll());
            }
        }
    }
}