using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;



namespace E_Vita.Models
{
    public enum GenderType
    {
        Male,
        Female
    }
    public enum ChronicDiseases
    {
        Obesity,
        Hypertension,
        Hypotension,
        Diabetes,
        Smoker,
        Other
    }
    public class Patient
    {
        public string contact { get; set; }
        
        public ChronicDiseases diseases { get; set; }

        public string name { get; set; }

        public string Nationality { get; set; }

        public GenderType Gender { get; set; }

        public DateTime Birth_Date { get; set; }

        [Key] public int Patient_ID { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
