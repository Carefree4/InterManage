using System;
using System.Collections.Generic;

namespace InterManage.Business.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            using (var db = new ApplicationDbContext())
            {
                #region make a tone of employees

                db.Employees.AddOrUpdate(new Employee()
                {
                    FirstName = "Terrance",
                    LastName = "Towles",
                });

                db.Employees.AddOrUpdate(new Employee()
                {
                    FirstName = "Rosalinda",
                    LastName = "Rodi"
                });

                db.Employees.AddOrUpdate(new Employee()
                {
                    FirstName = "Vickey",
                    LastName = "Vassell"
                });

                db.Employees.AddOrUpdate(new Employee()
                {
                    FirstName = "Marlo",
                    LastName = "Moran"
                });

                db.Employees.AddOrUpdate(new Employee()
                {
                    FirstName = "Josue",
                    LastName = "Jarrells"
                });
                #endregion

                db.SaveChanges();

            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
