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
    /// Interaction logic for Doctor_Dashboard.xaml
    /// </summary>
    public partial class Doctor_Dashboard : Page
    {
        public Doctor_Dashboard()
        {
            InitializeComponent();
        }
        private void AddMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new TextBox for entering medication
            TextBox medicationTextBox = new TextBox();
            medicationTextBox.Margin = new Thickness(0, 5, 0, 5);
            medicationTextBox.Width = 200;
            medicationTextBox.Height = 30;

            // Add the new TextBox to the StackPanel
            MedicationStackPanel.Children.Add(medicationTextBox);
        }

        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Appointments());
        }

        private void Patient_data(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Patient_Data());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FinancePage());

        }
    }
}
