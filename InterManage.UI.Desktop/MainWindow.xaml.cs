using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private Employee GetSelectedEmployee() => (Employee) _employeeViewSource.View.CurrentItem;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeDataGrid();
            dg_EmployeeList.SelectionMode = DataGridSelectionMode.Single;
        }


        #region Employee list Data Grid

        private void RefreshEmployeeDataGrid()
        {
            _employeeViewSource = (CollectionViewSource) FindResource("EmployeeViewSource");
            _employeeViewSource.Source = Employee.GetEmployeeList();
        }

        #endregion

        #region Employee Info Flyout

        private void OpenEmployeeInfoFlyout() => fo_EmployeeInfo.IsOpen = true;
        private void CloseEmployeeInfoFlyout() => fo_EmployeeInfo.IsOpen = false;

        #region Create

        private void btn_AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            dg_EmployeeList.CurrentItem = null;
            OpenEmployeeInfoFlyout();
            RefreshEmployeeInfoFlyout(new Employee());
        }

        #endregion

        #region Retrieve
        
        private Employee GetEmployeeFromInfoFlyout() =>
            new Employee
            {
                Id = Guid.Empty,
                FirstName = tb_InfoFirstName.Text,
                LastName = tb_InfoLastName.Text
            };

        private void RefreshEmployeeInfoFlyout() => RefreshEmployeeInfoFlyout(GetSelectedEmployee());
        private void RefreshEmployeeInfoFlyout(Employee employee)
        {
            tb_InfoFirstName.Text = employee.FirstName;
            tb_InfoLastName.Text = employee.LastName;
        }

        private void dg_EmployeeList_CurrentCellChanged(object sender, EventArgs e)
        {
            // False then true animates it closeing then opening again
            CloseEmployeeInfoFlyout();
            OpenEmployeeInfoFlyout();
            RefreshEmployeeInfoFlyout();
        }

        #endregion

        #region Update

        private void btn_InfoSave_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = GetSelectedEmployee();

            var updatedEmployee = GetEmployeeFromInfoFlyout();
            updatedEmployee.Id = selectedEmployee.Id;

            selectedEmployee.Update(updatedEmployee);
            RefreshEmployeeDataGrid();
            CloseEmployeeInfoFlyout();
        }

        private void btn_InfoCancel_Click(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeInfoFlyout();
            fo_EmployeeInfo.IsOpen = false;
        }

        #endregion

        #region Delete

        #region Helpers

        private void HideBtn_DeleteEmployeesIfNull(Employee employee) =>
            btn_DeleteEmployee.Visibility = employee.Id.Equals(Guid.Empty) ? Visibility.Hidden : Visibility.Visible;

        private async Task<string> ConfirmEmployeeDelete(Employee employee) => await
            this.ShowInputAsync(
                "Are you sure you want to delete " + employee.FirstName + " " + employee.LastName + "?",
                "Type their first name to confirm");

        private async Task ShowSuccessfullEmployeeDelete(Employee employee) =>
            await
                this.ShowMessageAsync("Delete successfull",
                    employee.FirstName + " " + employee.LastName + " has been deleted.");

        private async Task ShowUnsuccessfullEmployeeDelete(Employee employee) => await
            this.ShowMessageAsync("Delete unsuccessfull",
                employee.FirstName + " " + employee.LastName + " has not been deleted.");

        #endregion

        private async void btn_DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employee = GetSelectedEmployee();

            HideBtn_DeleteEmployeesIfNull(employee);
            var name = await ConfirmEmployeeDelete(employee);

            if (name != null)
            {
                if (name.Equals(employee.FirstName))
                {
                    employee.Delete();
                    await ShowSuccessfullEmployeeDelete(employee);
                }
                else
                {
                    await ShowUnsuccessfullEmployeeDelete(employee);
                }
            }

            CloseEmployeeInfoFlyout();
            RefreshEmployeeDataGrid();
        }

        
        #endregion

        #endregion

    }
}
