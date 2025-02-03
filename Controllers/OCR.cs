using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tesseract;
using PdfiumViewer;
using System.Drawing;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using E_Vita.Interfaces.Repository;
using System.Windows.Controls;
using E_Vita.Models;
namespace E_Vita.Controllers
{
    internal class OCR
    {

        private readonly IRepository<Medical_Record> _MedicalRepository;
        private readonly IRepository<Patient> _PatientRepository;
        private readonly IRepository<OCRPdfRead> _OCRrEPO;


        public OCR()
        {
            var services = ((App)Application.Current)._serviceProvider;
            _MedicalRepository = services.GetService<IRepository<Medical_Record>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _PatientRepository = services.GetService<IRepository<Patient>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _OCRrEPO = services.GetService<IRepository<OCRPdfRead>>() ?? throw new InvalidOperationException("Data helper service is not available");
        }
    private string PerformOCR(string imagePath)
        {
            try
            {
                string tessDataPath = @"D:\NU\fifth semester\Clinical\E-Vita\Assets\OCR_traine_data\";
                string languages = "eng+ara";

                using (var engine = new TesseractEngine(tessDataPath, languages, EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during OCR: {ex.Message}", "OCR Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }

    public void ConvertPdfToImages(string pdfPath, string outputDirectory)
    {
        using (var document = PdfDocument.Load(pdfPath))
        {
            for (int i = 0; i < document.PageCount; i++) // Loop through all pages
            {
                using (var image = document.Render(i, 300, 300, true)) // Render each page
                {
                    string outputImagePath = $"{outputDirectory}/page_{i + 1}.png";
                    image.Save(outputImagePath, System.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine($"Page {i + 1} saved to {outputImagePath}");
                }
            }
        }
    }


        public async void ProcessPdfWithOCR(string pdfPath,int patient_IDd)
        {
            var patient = await _PatientRepository.GetByIdAsync(patient_IDd);

            ConvertPdfToImages(pdfPath, "D:\\NU\\fifth semester\\Clinical\\Project_OCR");
            string outputDirectory = "D:\\NU\\fifth semester\\Clinical\\Project_OCR";
            OCR ocr = new OCR();
            for (int i = 1; ; i++) // Process until no more pages
            {
                string imagePath = $"{outputDirectory}/page_{i}.png";
                if (!System.IO.File.Exists(imagePath)) break; // Stop if the image does not exist

                string text = ocr.PerformOCR(imagePath);

                // Save OCR result to the database
                var ocrPdfRead = new OCRPdfRead
                {
                    FileName = $"page_{i}.png",
                    FileData = await System.IO.File.ReadAllBytesAsync(imagePath),
                    ContentType = "image/png",
                    ExtractedText = text,
                    PatientId = patient_IDd,
                    UploadedAt = DateTime.Now
                };
                //for loop to delete the images after reading them
                System.IO.File.Delete(imagePath);
                // Save the extracted text to a file
                string textFilePath = $"{outputDirectory}/page_{i}.txt";
                await System.IO.File.WriteAllTextAsync(textFilePath, text);

                await _OCRrEPO.AddAsync(ocrPdfRead);
            }
        }

    }
}



//Trash code 
//public string ConvertPdfToImage(string pdfPath, int pageNumber, string outputImagePath)
//{
//    using (var document = PdfDocument.Load(pdfPath))
//    {
//        using (var image = document.Render(pageNumber - 1, 300, 300, true)) // Render page 1 (zero-based index)
//        {
//            image.Save(outputImagePath, System.Drawing.Imaging.ImageFormat.Png);
//        }
//    }
//    return outputImagePath;
//}