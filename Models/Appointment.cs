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
        public int Appointment_ID { get; set; }
        public DateTime Date { get; set; } 
        public bool Status { get; set; }
        public string Time { get; set; }

        //foreign keys
        public int Patient_ID { get; set; }
        public int Doctor_ID { get; set; }

        public Patient Patient_appointment { get; set; } // Navigation property
        public Doctor Doctor_appointment { get; set; } // Navigation property

    }
}
