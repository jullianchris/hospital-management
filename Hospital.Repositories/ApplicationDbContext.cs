using Hospital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<HospitalInfo> HospitalInfos { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineReport> MedicineReports { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TestPrice> TestPrices { get; set; }
        public DbSet<Timing> Timings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Example for 'Bill' entity
            modelBuilder.Entity<Bill>()
                .Property(b => b.Advance)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Bill>()
                .Property(b => b.TotalBill)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Bill>()
             .Property(b => b.MedicineCharge)
             .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Bill>()
             .Property(b => b.RoomCharge)
             .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Bill>()
                          .Property(b => b.OperationCharge)
                          .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Medicine>()
                         .Property(b => b.Cost)
                         .HasColumnType("decimal(18, 2)");
            
            modelBuilder.Entity<TestPrice>()
                         .Property(b => b.Price)
                         .HasColumnType("decimal(18, 2)");


            modelBuilder.Entity<Payroll>()
                         .Property(b => b.Salary)
                         .HasColumnType("decimal(18, 2)");
            
            modelBuilder.Entity<Payroll>()
                         .Property(b => b.NetSalary)
                         .HasColumnType("decimal(18, 2)");
            
            modelBuilder.Entity<Payroll>()
                         .Property(b => b.HourlySalary)
                         .HasColumnType("decimal(18, 2)");
            
            modelBuilder.Entity<Payroll>()
                         .Property(b => b.BonusSalary)
                         .HasColumnType("decimal(18, 2)");
            
            modelBuilder.Entity<Payroll>()
                         .Property(b => b.Compensation)
                         .HasColumnType("decimal(18, 2)");


            modelBuilder.Entity<IdentityUserLogin<string>>()
                        .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            base.OnModelCreating(modelBuilder);
        }
    }
}
