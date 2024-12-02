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
    class Prescription
    {
        [Key]
        public int Prescription_ID { get; set; }
        public DateTime Date { get; set; }
        public  int Dosage { get; set; }
        public int Patient_ID { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient patient_id { get; set; }

    }
}
