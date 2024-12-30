using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Vita.Models
{
    public class OCRPdfRead
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileData { get; set; }

        public string ContentType { get; set; }

        public string ExtractedText { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public int? PatientId { get; set; } // Optional Foreign Key

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } // This remains for reference but not assumed to be bidirectional.
    }
}
