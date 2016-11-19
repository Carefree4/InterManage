using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace InterManage.Business
{
    [Table("Shifts")]
    public partial class Shift
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }

        public virtual Employee Employee { get; set; }
    }

    public partial class Shift
    {
        public void Update(Shift appendedShift)
        {
            using (var db = new ApplicationDbContext())
            {
                // So the ID doesnt change
                appendedShift.Id = Id;
                db.Shifts.AddOrUpdate(appendedShift);
                db.SaveChanges();
            }
        }

        public void Create(DateTime startTime, DateTime endTime, Employee employee)
        {
            using (var db = new ApplicationDbContext())
            {
                var s = new Shift()
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    Employee = employee
                };

                db.Shifts.Add(s);
                db.SaveChanges();
            }
        } 
    }
}
