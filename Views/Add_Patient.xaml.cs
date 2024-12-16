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
namespace E_Vita
{
    /// <summary>
    /// Interaction logic for Add_Patient.xaml
    /// </summary>
    public partial class Add_Patient : Page
    {
        public Add_Patient()
        {
            InitializeComponent();
            LoadNationalities();
        }
        private void LoadNationalities()
        {
            // Path to the JSON file
            string filePath = "D:\\NU\\fifth semester\\Clinical\\E-Vita\\E-Vita\\Assets\\Json_files\\nationalities-common.json";

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
        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Appointments());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Finance());
        }
    }
}
