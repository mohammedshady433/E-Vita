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
    public class Appointment
    {
        public DateTime Date { get; set; } 
        public bool Status { get; set; }


        public int Patient_ID { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient Patient { get; set; } // Navigation property



        public int Doctor_ID { get; set; }

        [ForeignKey("Doctor_ID")]
        public Doctor Doctor { get; set; } // Navigation property

    }
}
