using InterManage.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterManage.BusinessTests
{
    [TestClass()]
    public class EmployeeTests
    {
        [TestMethod()]
        public void GetEmployeeLsitTest()
        {
            var e = Employee.GetEmployeeLsit();
            Assert.IsNotNull(e);
        }
    }
}