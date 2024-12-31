using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using LiveCharts.Wpf;
using MaterialDesignExtensions.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using PdfiumViewer;
using E_Vita.Controllers;

namespace E_Vita.Views
{
    /// <summary>
    /// Interaction logic for Patient_info.xaml
    /// </summary>
    public partial class Patient_info : Page
    {
        private readonly IRepository<Medical_Record> _MedicalRepository;
        private readonly IRepository<Patient> _PatientRepository;
        private readonly IRepository<LabTest> _LabTestRepo;
        public Patient_info()
        {
            InitializeComponent();
            var services = ((App)Application.Current)._serviceProvider;
            _MedicalRepository = services.GetService<IRepository<Medical_Record>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _PatientRepository = services.GetService<IRepository<Patient>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _LabTestRepo = services.GetService<IRepository<LabTest>>() ?? throw new InvalidOperationException("Data helper service is not available");
        }


        private Patient _patient;
        public Patient_info(Patient patient)
        {
            InitializeComponent();

            _patient = patient;
            var services = ((App)Application.Current)._serviceProvider;
            _MedicalRepository = services.GetService<IRepository<Medical_Record>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _PatientRepository = services.GetService<IRepository<Patient>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _LabTestRepo = services.GetService<IRepository<LabTest>>() ?? throw new InvalidOperationException("Data helper service is not available");
            DisplayPatientInfo();
            LoadPatientHistoryAsync(_patient.Patient_ID);
        }

        private void DisplayPatientInfo()
        {
            NameTextBlock.Text = _patient.name;
            var age = DateTime.Now.Year - _patient.Birth_Date.Year;
            DOBTextBlock.Text = age.ToString();
            Gender_Box.Text = _patient.Gender.ToString();
            NationalityTextBlock.Text = _patient.Nationality;
            IdTextBlock.Text = _patient.Patient_ID.ToString();
        }

        // Save the medical record to the database
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> radiologies = new List<string>();
            List<string> labtests = new List<string>();

            // Get the medications from the TextBoxes
            string medications = string.Join(", ", _medicationTextBoxes.Select(tb => tb.Text));
            // Create a new Medical_Record object
            Medical_Record medicalRecord = new Medical_Record();

            medicalRecord.Date = DateTime.Now;
            medicalRecord.Future_Plan = Future_planText.Text;
            medicalRecord.Disease = Local_examinationText.Text;
            medicalRecord.Medication = medications;
            medicalRecord.Surgery = SurgeriesText.Text;
            medicalRecord.Family_History = Family_HistoryText.Text;
            medicalRecord.reason_for_visit = ReasonForVisitTextBox.Text;
            medicalRecord.Patient_ID = _patient.Patient_ID;
            if (X_Ray_checkBox.IsChecked == true)
            {
                radiologies.Add("X-Ray");
            }
            if (MRI_checkBox.IsChecked == true)
            {
                radiologies.Add("MRI");
            }
            if (CT_checkBox.IsChecked == true)
            {
                radiologies.Add("CT");
            }
            if (US_checkBox.IsChecked == true)
            {
                radiologies.Add("Ultrasound");
            }
            if (other_checkBox.IsChecked == true)
            {
                radiologies.Add(RadiologyTextBox.Text);
            }

            // Get the lab tests from the CheckBoxes
            if (Hep_checkBox.IsChecked == true)
            {
                labtests.Add("Hepatitis B/C");
            }
            if (CBC_checkBox.IsChecked == true)
            {
                labtests.Add("CBC");
            }
            if (Proth_checkBox.IsChecked == true)
            {
                labtests.Add("Proth Time and Content");
            }
            if (Urea_checkBox.IsChecked == true)
            {
                labtests.Add("Blood Urea and Creatine");
            }
            if (SGOT_checkBox.IsChecked == true)
            {
                labtests.Add("SGPT and SGOT");
            }
            if (Fasting_checkBox.IsChecked == true)
            {
                labtests.Add("Fasting pp blood");
            }
            if (Glyco_checkBox.IsChecked == true)
            {
                labtests.Add("Glycodytit Hb");
            }
            if (Other_Bloodtests_checkBox.IsChecked == true)
            {
                labtests.Add(BloodTestTextBox.Text);
            }


            // Get the radiologies from the CheckBoxes
            string radiologiesstr = string.Join(", ", radiologies);
            //Get the lab tests from the CheckBoxes
            string labtestsstr = string.Join(", ", labtests);

            medicalRecord.Radiology = radiologiesstr;
            medicalRecord.Lab = labtestsstr;

            // Add the medical record to the database
            await _MedicalRepository.AddAsync(medicalRecord);
            // Display a success message
            MessageBox.Show("Medical record saved successfully.");
        }
        private void ShowSurgeriesCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SurgeriesPanel.Visibility = Visibility.Visible;
        }

        private void ShowSurgeriesCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SurgeriesPanel.Visibility = Visibility.Collapsed;
        }

        private List<TextBox> _medicationTextBoxes = new List<TextBox>();
        private void AddMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new TextBox for entering medication
            MedicationsWindow medWindow = new MedicationsWindow();
            medWindow.ShowDialog();
            // Check if a drug was selected
            if (medWindow.SelectedDrug != null)
            {
                // Create a new TextBox for entering medication
                TextBox medicationTextBox = new TextBox
                {
                    Name = "MedicationTextBox",
                    Margin = new Thickness(0, 5, 0, 5),
                    Width = 300,
                    Height = 30,
                    FontSize = 16,
                    Background = (Brush)new BrushConverter().ConvertFromString("#e1b184"),
                    Text = medWindow.SelectedDrug.Tradename // Set the TextBox text to the selected drug's tradename
                };

                // Add the new TextBox to the StackPanel
                MedicationStackPanel.Children.Add(medicationTextBox);

                // Add the TextBox to the collection
                _medicationTextBoxes.Add(medicationTextBox);
            }
            else
            {
                MessageBox.Show("No medication was selected.");
            }
        }

        private async Task LoadPatientHistoryAsync(int patientId)
        {
            try
            {
                var medicalRecords = await _MedicalRepository.GetAllAsync();
                medicalRecords = medicalRecords.Where(record => record.Patient_ID == patientId).ToList();

                if (medicalRecords.Any())
                {
                    PatientHistoryDataGrid.ItemsSource = medicalRecords;
                }
                else
                {
                    MessageBox.Show("No medical history found for this patient.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                var fileName = System.IO.Path.GetFileName(filePath);
                var fileData = File.ReadAllBytes(filePath);
                var contentType = GetContentType(filePath);

                SaveImageToDatabase(fileName, fileData, contentType);
            }
        }
        private string GetContentType(string filePath)
        {
            var extension = System.IO.Path.GetExtension(filePath).ToLowerInvariant();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".bmp":
                    return "image/bmp";
                case ".tif":
                    return "image/tif";
                default:
                    throw new NotSupportedException($"File extension {extension} is not supported");
            }
        }
        private async void SaveImageToDatabase(string fileName, byte[] fileData, string contentType)
        {
            try
            {
                var newLabTest = new LabTest
                {
                    FileName = fileName,
                    ImageData = fileData,
                    ContentType = contentType,
                    UploadedAt = DateTime.Now,
                    PatientId = _patient.Patient_ID
                };

                await _LabTestRepo.AddAsync(newLabTest);
                MessageBox.Show("Image saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UploadPDFButton_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image and PDF Files (*.bmp;*.jpg;*.jpeg;*.png;*.pdf)|*.bmp;*.jpg;*.jpeg;*.png;*.pdf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Check file extension to determine type
                string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

                if (fileExtension == ".pdf")
                {
                    // Handle PDF files
                    using (var document = PdfDocument.Load(filePath))
                    {
                        var singlepage = new OCR();
                        singlepage.ProcessPdfWithOCR(filePath, _patient.Patient_ID);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid file type. Please select a PDF file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private async void SaveandPrintBtn_Click(object sender, RoutedEventArgs e)
        {

                List<string> radiologies = new List<string>();
                List<string> labtests = new List<string>();

                // Get the medications from the TextBoxes
                string medications = string.Join(", ", _medicationTextBoxes.Select(tb => tb.Text));
                // Create a new Medical_Record object
                Medical_Record medicalRecord = new Medical_Record();

                medicalRecord.Date = DateTime.Now;
                medicalRecord.Future_Plan = Future_planText.Text;
                medicalRecord.Disease = Local_examinationText.Text;
                medicalRecord.Medication = medications;
                medicalRecord.Surgery = SurgeriesText.Text;
                medicalRecord.Family_History = Family_HistoryText.Text;
                medicalRecord.reason_for_visit = ReasonForVisitTextBox.Text;
                medicalRecord.Patient_ID = _patient.Patient_ID;
                if (X_Ray_checkBox.IsChecked == true)
                {
                    radiologies.Add("X-Ray");
                }
                if (MRI_checkBox.IsChecked == true)
                {
                    radiologies.Add("MRI");
                }
                if (CT_checkBox.IsChecked == true)
                {
                    radiologies.Add("CT");
                }
                if (US_checkBox.IsChecked == true)
                {
                    radiologies.Add("Ultrasound");
                }
                if (other_checkBox.IsChecked == true)
                {
                    radiologies.Add(RadiologyTextBox.Text);
                }

                // Get the lab tests from the CheckBoxes
                if (Hep_checkBox.IsChecked == true)
                {
                    labtests.Add("Hepatitis B/C");
                }
                if (CBC_checkBox.IsChecked == true)
                {
                    labtests.Add("CBC");
                }
                if (Proth_checkBox.IsChecked == true)
                {
                    labtests.Add("Proth Time and Content");
                }
                if (Urea_checkBox.IsChecked == true)
                {
                    labtests.Add("Blood Urea and Creatine");
                }
                if (SGOT_checkBox.IsChecked == true)
                {
                    labtests.Add("SGPT and SGOT");
                }
                if (Fasting_checkBox.IsChecked == true)
                {
                    labtests.Add("Fasting pp blood");
                }
                if (Glyco_checkBox.IsChecked == true)
                {
                    labtests.Add("Glycodytit Hb");
                }
                if (Other_Bloodtests_checkBox.IsChecked == true)
                {
                    labtests.Add(BloodTestTextBox.Text);
                }

                // Get the radiologies from the CheckBoxes
                string radiologiesstr = string.Join(", ", radiologies);
                //Get the lab tests from the CheckBoxes
                string labtestsstr = string.Join(", ", labtests);

                medicalRecord.Radiology = radiologiesstr;
                medicalRecord.Lab = labtestsstr;

                // Add the medical record to the database
                await _MedicalRepository.AddAsync(medicalRecord);
                // Display a success message
                MessageBox.Show("Medical record saved successfully.");

                // Create a PDF document
                using (var document = new PdfSharp.Pdf.PdfDocument())
                {
                    var page = document.AddPage();
                    var gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                    var font = new PdfSharp.Drawing.XFont("Verdana", 14);

                    // Load the logo image
                    var logoPath = "D:\\NU\\fifth semester\\Clinical\\E-Vita\\Assets\\E-Vita logo-01.png"; // Replace with the actual path to your logo image
                    var logo = PdfSharp.Drawing.XImage.FromFile(logoPath);
                    // Draw the logo image on the PDF
                gfx.DrawImage(logo, 20, 20, 100, 50);

                // Add content to the PDF
                gfx.DrawString("Prescription", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, 80, page.Width, page.Height), PdfSharp.Drawing.XStringFormats.TopCenter);
                gfx.DrawString("Patient Name:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 120));
                gfx.DrawString($"{_patient.name}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 120));
                gfx.DrawString($"Date: {medicalRecord.Date}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 140));
                gfx.DrawString($"Future Plan:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 160));
                gfx.DrawString($"{medicalRecord.Future_Plan}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 160));
                gfx.DrawString($"Disease:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 180));
                gfx.DrawString($"{medicalRecord.Disease}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 180));
                gfx.DrawString($"Medication:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 200));
                gfx.DrawString($"{medicalRecord.Medication}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 200));
                gfx.DrawString($"Surgery:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 220));
                gfx.DrawString($"{medicalRecord.Surgery}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 220));
                gfx.DrawString($"Radiology:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 240));
                gfx.DrawString($"{medicalRecord.Radiology}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 240));
                gfx.DrawString($"Lab tests:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 260));
                gfx.DrawString($"{medicalRecord.Lab}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 260));
                gfx.DrawString($"Family History:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 280));
                gfx.DrawString($"{medicalRecord.Family_History}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 280));
                gfx.DrawString($"Reason for Visit:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 300));
                gfx.DrawString($"{medicalRecord.reason_for_visit}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 300));
                gfx.DrawString($"Patient ID:", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(20, 320));
                gfx.DrawString($"{medicalRecord.Patient_ID}", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(150, 320));

                // Draw border
                var borderPen = new PdfSharp.Drawing.XPen(PdfSharp.Drawing.XColors.Black, 1);
                gfx.DrawRectangle(borderPen, 10, 10, page.Width - 20, page.Height - 20);

                // Use SaveFileDialog to let the user choose the location
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = $"MedicalRecord_{medicalRecord.Patient_ID}_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Filter = "PDF files (*.pdf)|*.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    document.Save(saveFileDialog.FileName);
                    MessageBox.Show($"PDF saved successfully as {saveFileDialog.FileName}.");
                }
                }
            }
        }
    }
    


