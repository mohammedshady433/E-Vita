using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace E_Vita.Models
{
    public class Nurse
    {
        [Key] public int Nurse_ID { get; set; }
        public string User_name { get; set; }
        public string Name { get; set; }
        public string speciality { get; set; }
        public string pass { get; set; }


        public int Patient_ID { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient patient_id { get; set; }


        public int Doctor_ID { get; set; }

        [ForeignKey("Doctor_ID")]
        public Doctor doctor_id { get; set; }




    }
}
