using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InterManage.Repository.Persistence;

namespace InterManage.Migrations
{
    [DbContext(typeof(InterManageDbContext))]
    [Migration("20170311062210_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("InterManage.Model.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("InterManage.Model.CustomerPresence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CustomerId");

                    b.Property<Guid?>("EmployeeId");

                    b.Property<DateTime>("TimeOfCheckIn");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("CustomerPresences");
                });

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

            modelBuilder.Entity("InterManage.Model.CustomerPresence", b =>
                {
                    b.HasOne("InterManage.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("InterManage.Model.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
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
