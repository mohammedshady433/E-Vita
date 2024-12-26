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
using System.Windows.Shapes;

namespace E_Vita
{
    /// <summary>
    /// Interaction logic for BookAppointmentWindow.xaml
    /// </summary>
    public partial class BookAppointmentWindow : Window
    {
        private readonly IRepository<Appointment> _Appointment;
        private readonly IRepository<Patient> _Add_patient;

        public BookAppointmentWindow()
        {
            InitializeComponent();
            var services = ((App)Application.Current)._serviceProvider;
            _Appointment = services.GetService<IRepository<Appointment>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _Add_patient = services.GetService<IRepository<Patient>>() ?? throw new InvalidOperationException("Data helper service is not available");

        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the popup
        }
        private async void ConfirmAppointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment newappointment = new Appointment();
            Patient patient_data = new Patient();

            int patientID = int.Parse(PatientNameTextBox.Text);
            patient_data = await _Add_patient.GetByIdAsync(patientID);
            string patientName = "";
            if (patient_data != null)
            {
                patientName = patient_data.name;
            } 
            else 
            {
                MessageBox.Show("Patient not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime? appointmentDate = AppointmentDatePicker.SelectedDate;
            string appointmentTime = (AppointmentTimeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrWhiteSpace(PatientNameTextBox.Text) || appointmentDate == null || string.IsNullOrWhiteSpace(appointmentTime))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (appointmentDate < DateTime.Now)
            {
                MessageBox.Show("Please select a valid date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Check if the selected date and time are already reserved
            if (await IsAppointmentReserved(appointmentDate.Value, appointmentTime))
            {
                MessageBox.Show("The selected date and time are already reserved.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            newappointment.Patient_ID = patientID;
            newappointment.Date = appointmentDate.Value.Date;
            newappointment.Doctor_ID = int.Parse(Doctor_ID.Text);
            newappointment.Time = appointmentTime;

            await _Appointment.AddAsync(newappointment);
            MessageBox.Show($"Appointment for {patientName} on {appointmentDate.Value.ToShortDateString()} at {appointmentTime} confirmed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
        private async Task<bool> IsAppointmentReserved(DateTime date, string time)
        {
            var appointments = await _Appointment.GetAllAsync();
            return appointments.Any(a => a.Date.Date == date.Date && a.Time == time);
        }
    }
}
