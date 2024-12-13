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
        public BookAppointmentWindow()
        {
            InitializeComponent();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the popup
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
