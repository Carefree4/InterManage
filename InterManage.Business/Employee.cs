using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace InterManage.Business
{
    [Table("Employees")]
    public partial class Employee
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public virtual List<Shift> Shifts { get; set; }
    }

    public partial class Employee
    {
        public static void Create(string firstName, string lastName)
        {
            using (var db = new ApplicationDbContext())
            {
                var e = new Employee()
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                db.Employees.Add(e);
                db.SaveChanges();
            }
        }

        public static List<Employee> GetEmployeeLsit()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Employees.ToList();
            }
        }
    }
}
