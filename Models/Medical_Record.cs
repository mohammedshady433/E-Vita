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
    public class Medical_Record
    {
        public DateTime Date { get; set; }
        public string Future_Plan { get; set; }
        public string Disease { get; set; }
        public string Medication { get; set; }
        public string Surgery { get; set; }
        public string Family_History { get; set; }

        [Key]
        public int Record_ID { get; set; }


        public int Patient_ID { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient patient_id { get; set; }



    }
}
