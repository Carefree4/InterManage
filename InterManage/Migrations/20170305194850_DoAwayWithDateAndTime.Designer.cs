using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InterManage.Repository.Persistence;

namespace InterManage.Migrations
{
    [DbContext(typeof(InterManageDbContext))]
    [Migration("20170305194850_DoAwayWithDateAndTime")]
    partial class DoAwayWithDateAndTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("InterManage.Model.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Error");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("InterManage.Model.Shift", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssignedEmployeeId");

                    b.Property<DateTime>("End");

                    b.Property<string>("Error");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.HasIndex("AssignedEmployeeId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("InterManage.Model.Shift", b =>
                {
                    b.HasOne("InterManage.Model.Employee", "AssignedEmployee")
                        .WithMany("Shifts")
                        .HasForeignKey("AssignedEmployeeId")
                        .HasConstraintName("ForeignKey_Employee_Shift");
                });
        }
    }
}
