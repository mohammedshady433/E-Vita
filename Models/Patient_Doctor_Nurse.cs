using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace E_Vita.Models
{
    public class Patient_Doctor_Nurse
    {
        // Foreign Key Properties
        public int Nurse_ID { get; set; }
        public int Patient_ID { get; set; }
        public int Doctor_ID { get; set; }

        // Navigation Properties
        [ForeignKey("Nurse_ID")]
        public Nurse Nurse { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient Patient { get; set; }

        [ForeignKey("Doctor_ID")]
        public Doctor Doctor { get; set; }


    }
}
