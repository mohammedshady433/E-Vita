using Azure;
using E_Vita.Interfaces.Repository;
using E_Vita.Models;
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

namespace E_Vita
{
    /// <summary>
    /// Interaction logic for Patient_Data.xaml
    /// </summary>
    public partial class Patient_Data : Page
    {
        private readonly IRepository<Medical_Record> _search_for_patient;

        public Patient_Data()
        {
            InitializeComponent();
            var services = ((App)Application.Current)._serviceProvider;
            _search_for_patient = services.GetService<IRepository<Medical_Record>>() ?? throw new InvalidOperationException("Data helper service is not available");

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (search_by_id.CanGoBack)
            {
                search_by_id.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (search_by_id.CanGoForward)
            {
                search_by_id.GoForward();
            }
        }

        private async Task LoadPatientHistoryAsync(int patientId)
        {
            try
            {
                var medicalRecords = await _search_for_patient.GetByIdPatientAsync(patientId);

                if (medicalRecords.Any())
                {
                    PatientHistoryDataGrid.ItemsSource = medicalRecords.Select(record => new
                    {
                        Plan = record.Future_Plan,
                        Surgery = record.Surgery,
                        Reason_of_visit = record.reason_for_visit,
                        Medication = record.Medication,
                        MedicalDiseases = record.Disease,
                        Family_History = record.Family_History
                    });
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
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(SearchForPatientTextBox.Text, out int patientId))
            {
                await LoadPatientHistoryAsync(patientId);
            }
            else
            {
                MessageBox.Show("Please enter a valid Patient ID.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
    }
    

