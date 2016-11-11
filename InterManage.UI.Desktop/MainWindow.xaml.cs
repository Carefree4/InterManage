using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using InterManage.Business;
using MahApps.Metro.Controls;

namespace InterManage
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private CollectionViewSource _employeeViewSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _employeeViewSource = (CollectionViewSource) FindResource("employeeViewSource");
            _employeeViewSource.Source = Employee.GetEmployeeLsit();
            // Load data by setting the CollectionViewSource.Source property:
            // _employeeViewSource.Source = [generic data source]
        }

        #region Employee Info Flyout

        private void dg_EmployeeList_CurrentCellChanged(object sender, EventArgs e)
        {
            // False then true animates it closeing then opening again
            fo_EmployeeInfo.IsOpen = false;
            fo_EmployeeInfo.IsOpen = true;

            RefreshEmployeeInfoFlyout();
        }

        private void btn_InfoSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_InfoCancel_Click(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeInfoFlyout();
        }

        private void RefreshEmployeeInfoFlyout()
        {
            var selectedEmployee = (Employee) _employeeViewSource.View.CurrentItem;
            tb_InfoFirstName.Text = selectedEmployee.FirstName;
            tb_InfoLastName.Text = selectedEmployee.LastName;
        }

        #endregion
    }
}
