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
        public Patient_info()
        {
            InitializeComponent();
        }
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
                    Margin = new Thickness(0, 5, 0, 5),
                    Width = 300,
                    Height = 30,
                    FontSize = 16,
                    Background = (Brush)new BrushConverter().ConvertFromString("#FFBBE1FA"),
                    Text = medWindow.SelectedDrug.Tradename // Set the TextBox text to the selected drug's tradename
                };

                // Add the new TextBox to the StackPanel
                MedicationStackPanel.Children.Add(medicationTextBox);
            }
            else
            {
                MessageBox.Show("No medication was selected.");
            }
        }
    }

}
