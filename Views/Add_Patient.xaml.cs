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
using System.IO;
using System.Text.Json;
using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using Mysqlx.Crud;
using Microsoft.Extensions.DependencyInjection;
namespace E_Vita
{
    /// <summary>
    /// Interaction logic for Add_Patient.xaml
    /// </summary>
    public partial class Add_Patient : Page
    {
        private readonly IRepository<Patient> _Add_patient;

        public Add_Patient()
        {
            InitializeComponent();
            LoadNationalities();
            var services = ((App)Application.Current)._serviceProvider;
            _Add_patient = services.GetService<IRepository<Patient>>() ?? throw new InvalidOperationException("Data helper service is not available");
        }
        private void LoadNationalities()
        {
            // Path to the JSON file
            string filePath = "D:\\NU\\fifth semester\\Clinical\\E-Vita\\Assets\\Json_files\\nationalities-common.json";

            // Read the JSON file
            string jsonContent = File.ReadAllText(filePath);

            // Parse the JSON to extract the nationalities
            var data = JsonSerializer.Deserialize<NationalityData>(jsonContent);

            // Bind the nationalities to the ComboBox
            NationalitiesComboBox.ItemsSource = data.Nationalities;
        }
        // Helper class to match the JSON structure
        public class NationalityData
        {
            public List<string> Nationalities { get; set; }
        }

        private async void Addbtn(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_txt.Text) ||
                string.IsNullOrWhiteSpace(ID_Txt.Text) ||
                string.IsNullOrWhiteSpace(Phone_Number.Text) ||
                dateofbirth.SelectedDate == null ||
                string.IsNullOrWhiteSpace(NationalitiesComboBox.Text) ||
                (Malecheck.IsChecked == false && Femalecheck.IsChecked == false))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            else
            {
                LoadingProgressBar.Visibility = Visibility.Visible;
                LoadingTextBlock.Visibility = Visibility.Visible;
                Patient varpatient = new Patient();
                varpatient.name = Name_txt.Text;
                varpatient.Patient_ID = int.Parse(ID_Txt.Text);
                varpatient.contact = Phone_Number.Text;
                varpatient.Birth_Date = dateofbirth.SelectedDate.Value;
                varpatient.Nationality = NationalitiesComboBox.Text;
                if (Malecheck.IsChecked == true)
                {
                    varpatient.Gender = GenderType.Male;
                }
                else
                {
                    varpatient.Gender = GenderType.Female;
                }
                if (Obesity.IsChecked == true)
                {
                    varpatient.diseases = ChronicDiseases.Obesity;
                }
                else if (Hypertension.IsChecked == true)
                {
                    varpatient.diseases = ChronicDiseases.Hypertension;
                }
                else if (Hypotension.IsChecked == true)
                {
                    varpatient.diseases = ChronicDiseases.Hypotension;
                }
                else if (Diabetes.IsChecked == true)
                {
                    varpatient.diseases = ChronicDiseases.Diabetes;
                }
                else if (Smoker.IsChecked == true)
                {
                    varpatient.diseases = ChronicDiseases.Smoker;
                }
                else if (Other.IsChecked == true)
                {
                    varpatient.diseases = ChronicDiseases.Other;
                }
                await _Add_patient.AddAsync(varpatient);

                // Hide loading message
                LoadingProgressBar.Visibility = Visibility.Collapsed;
                LoadingTextBlock.Visibility = Visibility.Collapsed;
                //to show the message of verfication
                MessageBox.Show("Patient Added Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
