using System;
using System.Windows;
using System.Windows.Controls;

namespace E_Vita
{
    public partial class BookAppointmentWindow : Window
    {
        public BookAppointmentWindow()
        {
            InitializeComponent();
        }

        private void ConfirmAppointment_Click(object sender, RoutedEventArgs e)
        {
            string patientName = PatientNameTextBox.Text;
            DateTime? appointmentDate = AppointmentDatePicker.SelectedDate;
            string appointmentTime = (AppointmentTimeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(patientName) || appointmentDate == null || string.IsNullOrWhiteSpace(appointmentTime))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show($"Appointment for {patientName} on {appointmentDate.Value.ToShortDateString()} at {appointmentTime} confirmed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
