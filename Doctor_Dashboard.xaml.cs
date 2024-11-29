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
        // Event handler for adding new medication textboxes
        private void AddMedicationTextbox(object sender, RoutedEventArgs e)
        {
            // Create a new StackPanel for the new medication entry
            StackPanel newMedicationEntry = new StackPanel();
            newMedicationEntry.Orientation = Orientation.Horizontal;
            newMedicationEntry.Margin = new Thickness(5);

            // Create a new TextBox for the medication name
            TextBox newMedicationTextbox = new TextBox();
            newMedicationTextbox.Width = 400;
            newMedicationTextbox.Height = 40;
            newMedicationTextbox.Background = new SolidColorBrush(Color.FromArgb(255, 27, 38, 44)); // #1B262C
            newMedicationTextbox.Foreground = new SolidColorBrush(Color.FromArgb(255, 187, 225, 250)); // #FFBBE1FA
            newMedicationTextbox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 187, 223, 251)); // #BBDEFB


            // Add the TextBox and Button to the StackPanel
            newMedicationEntry.Children.Add(newMedicationTextbox);
            
            // Add the new StackPanel to the Medication StackPanel
            MedicationStackPanel.Children.Add(newMedicationEntry);
            newMedicationTextbox.Focus();
        }

    }
}
