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
        public int Doctor_ID { get; set; }

        [ForeignKey("Doctor_ID")]
        public Doctor doctor_id { get; set; }

        [Key]
        public int Nurse_ID { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }

    }
}
