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
    public class Doctor
    {
        [Key]
        public int Doctor_ID { get; set; }

        public int Name { get; set; }
        public string Speciality { get; set; }

        public string  User_Name { get; set; }

        public string Pass { get; set; }
        public ICollection<Appointment> Appointments { get; set; }


    }
}
