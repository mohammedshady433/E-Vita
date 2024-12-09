using System.Windows;
using System.Windows.Controls;

namespace E_Vita
{
    public partial class AddPatientWindow : Page
    {
        public AddPatientWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Patient registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            // Navigate to another page or perform another action
            NavigationService?.GoBack();
        }
    }
}