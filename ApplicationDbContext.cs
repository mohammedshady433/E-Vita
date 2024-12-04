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
            optionsBuilder.UseMySQL("Server=localhost;Database=E-Vita;User ID=root;Password=Moh@10042004;SslMode=Preferred;");
        }
        public DbSet<Models.Patient> patient_Datas { get; set; }
        public DbSet<Models.Doctor> Doctors { get; set; }
        public DbSet<Models.Medical_Record> Medical_Records { get; set; }
        public DbSet<Models.Patient_Doctor_Nurse> patient_Doctor_Nurses { get; set; }
        public DbSet<Models.Prescription> Prescriptions { get; set; }
        public DbSet<Models.Reset_Pass_Log> Reset_Pass_Logs { get; set; }
        public DbSet<Models.Nurse> Nurses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient_Doctor_Nurse>()
                .HasKey(pdn => new { pdn.Nurse_ID, pdn.Patient_ID, pdn.Doctor_ID });
        }


    }
}
