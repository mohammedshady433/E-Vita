using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace E_Vita
{
    public class Nurse
    {
        public int Nurse_ID{get; set; }
        public string User_name{get; set; }
        public string Name{get; set; }
        public string speciality {  get; set; }
        public string pass {  get; set; }
        [ForeignKey("Appointment")]
        public int patient_ID { get; set; }
        [ForeignKey("Appointment")]
        public int Doctor_ID { get; set; }

    }
}
