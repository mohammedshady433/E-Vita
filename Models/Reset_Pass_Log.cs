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
    public class Reset_Pass_Log
    {
        public DateTime Date { get; set; }

        public string New_Pass { get; set; }

        public string Admin_Pass { get; set; }
         
        /// <summary>
        ///  composite primary Key 
        /// </summary>
        public string User_Name { get; set; }
        public int Doc_ID { get; set; }

        [ForeignKey("Doc_ID")]
        public Doctor doc_id { get; set; }

        public int ID { get; set; }






    }
}
