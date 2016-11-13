using System;
using System.Windows;
using System.Windows.Data;
using InterManage.Business;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

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

        private Employee GetSelectedEmployee() => (Employee)_employeeViewSource.View.CurrentItem;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeDataGrid();
        }

        #region Employee list Data Grid

        private void RefreshEmployeeDataGrid()
        {
            _employeeViewSource = (CollectionViewSource)FindResource("EmployeeViewSource");
            _employeeViewSource.Source = Employee.GetEmployeeList();
        }

        #endregion

        #region Employee Info Flyout

        private void OpenEmployeeInfoFlyout() => fo_EmployeeInfo.IsOpen = true;
        private void CloseEmployeeInfoFlyout() => fo_EmployeeInfo.IsOpen = false;


        private void dg_EmployeeList_CurrentCellChanged(object sender, EventArgs e)
        {
            // False then true animates it closeing then opening again
            CloseEmployeeInfoFlyout();
            OpenEmployeeInfoFlyout();
            RefreshEmployeeInfoFlyout();
        }

        private void btn_InfoSave_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = GetSelectedEmployee();

            var updatedEmployee = new Employee()
            {
                Id = selectedEmployee.Id,
                FirstName = tb_InfoFirstName.Text,
                LastName = tb_InfoLastName.Text
            };

            selectedEmployee.Update(updatedEmployee);
            RefreshEmployeeDataGrid();
            CloseEmployeeInfoFlyout();
        }

        private void btn_InfoCancel_Click(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeInfoFlyout();
            fo_EmployeeInfo.IsOpen = false;
        }

        private void RefreshEmployeeInfoFlyout()
        {
            var selectedEmployee = GetSelectedEmployee();
            tb_InfoFirstName.Text = selectedEmployee.FirstName;
            tb_InfoLastName.Text = selectedEmployee.LastName;
        }

        private async void btn_DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employee = GetSelectedEmployee();
            var name = await this.ShowInputAsync("Are you sure you want to delete " + employee.FirstName + " " + employee.LastName + "?", "Type their first name to confirm");

            if (name.Equals(employee.FirstName))
            {
                employee.Delete();
                await this.ShowMessageAsync("Delete successfull", employee.FirstName + " " + employee.LastName + " has been deleted.");
            }
            else
            {
                await this.ShowMessageAsync("Delete unsuccessfull", employee.FirstName + " " + employee.LastName + " has not been deleted.");
            }

            RefreshEmployeeDataGrid();
        }
         
        #endregion
    }
}
