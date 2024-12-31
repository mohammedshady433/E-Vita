using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using MySql.EntityFrameworkCore;

namespace E_Vita
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=E-Vita;User ID=root;Password=P@ss01665;SslMode=Preferred;");
        }
        public DbSet<Models.Patient> patient_Datas { get; set; }
        public DbSet<Models.Doctor> Doctors { get; set; }
        public DbSet<Models.Medical_Record> Medical_Records { get; set; }
        public DbSet<Models.Patient_Doctor_Nurse> patient_Doctor_Nurses { get; set; }
        public DbSet<Models.Prescription> Prescriptions { get; set; }
        public DbSet<Models.Reset_Pass_Log> Reset_Pass_Logs { get; set; }
        public DbSet<Models.Nurse> Nurses { get; set; }
        public DbSet<Models.Appointment> Appointments_DB { get; set; }
        public DbSet<Models.LabTest> LabTests { get; set; }
        public DbSet<Models.OCRPdfRead> OCRPdfReads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient_Doctor_Nurse>()
                .HasKey(pdn => new { pdn.Nurse_ID, pdn.Patient_ID, pdn.Doctor_ID });

            // Define composite primary key
            modelBuilder.Entity<Appointment>()
            .HasKey(a => a.Appointment_ID);

            // Define Doctor-Appointments relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor_appointment)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.Doctor_ID)
                .OnDelete(DeleteBehavior.Cascade); // cascade delete

            // Define Patient-Appointments relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient_appointment)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.Patient_ID)
                .OnDelete(DeleteBehavior.Cascade); // cascade delete

            modelBuilder.Entity<LabTest>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<OCRPdfRead>()
                .HasOne(o => o.Patient)
                .WithMany() // No navigation property on Patient
                .HasForeignKey(o => o.PatientId);

        }


    }
}
