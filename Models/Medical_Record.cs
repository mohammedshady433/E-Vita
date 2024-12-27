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
    //public enum RadiologyType
    //{
    //    X_Ray,
    //    MRI,
    //    CT,
    //    Ultrasound,
    //    Other
    //}
    //public enum LabType
    //{
    //    Hepatitis_B_C,
    //    CBC,
    //    Proth_Time_and_Content,
    //    Urine_Test,
    //    Blood_Urea_and_Creatine,
    //    SGPT_and_SGOT,
    //    Fasting_pp_blood,
    //    Glycodytit_Hb,
    //    Other
    //}
    public class Medical_Record
    {
        public DateTime Date { get; set; }
        public string Future_Plan { get; set; }
        public string Disease { get; set; }
        public string Medication { get; set; }
        public string Surgery { get; set; }
        public string Radiology { get; set; }
        public string Lab { get; set; }

        public string Family_History { get; set; }
        public string reason_for_visit { get; set; }
        [Key]
        public int Record_ID { get; set; }

        public int Patient_ID { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient patient_id { get; set; }


    }
}
