using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using LiveCharts.Wpf;
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

namespace E_Vita.Views
{
    /// <summary>
    /// Interaction logic for Patient_info.xaml
    /// </summary>
    public partial class Patient_info : Page
    {
        private readonly IRepository<Medical_Record> _MedicalRepository;
        private readonly IRepository<Patient> _PatientRepository;
        public Patient_info()
        {
            InitializeComponent();
            var services = ((App)Application.Current)._serviceProvider;
            _MedicalRepository = services.GetService<IRepository<Medical_Record>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _PatientRepository = services.GetService<IRepository<Patient>>() ?? throw new InvalidOperationException("Data helper service is not available");
        }


        private Patient _patient;
        public Patient_info(Patient patient)
        {
            InitializeComponent();

            _patient = patient;
            var services = ((App)Application.Current)._serviceProvider;
            _MedicalRepository = services.GetService<IRepository<Medical_Record>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _PatientRepository = services.GetService<IRepository<Patient>>() ?? throw new InvalidOperationException("Data helper service is not available");
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
                    Background = (Brush)new BrushConverter().ConvertFromString("#FFBBE1FA"),
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
    }

}
