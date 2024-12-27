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
        public class LabTest
        {
            public int Id { get; set; }
            public string FileName { get; set; }
            public byte[] ImageData { get; set; }
            public string ContentType { get; set; }
            public DateTime UploadedAt { get; set; }
            public int PatientId { get; set; }

            [ForeignKey("Patient_ID")]
            public Patient Patient { get; set; }


        }
}
