using E_Vita.Controllers;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
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
    public partial class MedicationsWindow : Window
    {
        // Declare the DrugList property at the class level
        public List<Controllers.Drug> DrugList { get; set; }

        // Add a property to store the selected drug
        public Drug SelectedDrug { get; private set; }
        public MedicationsWindow()
        {
            InitializeComponent();
            DrugsController drug = new DrugsController();
            drug.LoadDrugs();
            DrugList = drug.Data.Drugs;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Logic for searching drugs using the SearchTextBox text
            string searchText = (sender as TextBox)?.Text;
            string query = SearchTextBox.Text.ToLower();
            var filteredList = DrugList.Where(d => d.Tradename.ToLower().Contains(query)).ToList();
            // Update the ComboBox with filtered results
            SearchResultsComboBox.ItemsSource = filteredList;
            SearchResultsComboBox.IsDropDownOpen = filteredList.Any(); // Open dropdown if any results
        }
        private void SearchResultsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SearchResultsComboBox.SelectedItem is Drug selectedDrug)
            {
                SelectedDrug = selectedDrug;

                // You can display more details of the selected drug in the UI here
                MessageBox.Show($"You selected: {selectedDrug.Tradename} \n Price={selectedDrug.new_price}");

                this.Close();
            }
        }
    }
}
