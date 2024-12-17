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
    /// Interaction logic for Reset_Password.xaml
    /// </summary>
    public partial class Reset_Password : Window
    {
        public Reset_Password()
        {
            InitializeComponent();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Admin credentials (assuming admin username = "admin" and password = "000")
            int adminPassword = 000;  // Admin password (example)
            string adminUsername = "admin"; // Admin username

            // Validate the input (password should be numeric)
            if (int.TryParse(txtPass.Password, out int password))
            {
                // Check if the entered credentials are correct
                if (password == adminPassword && txtUser.Text == adminUsername)
                {
                    MessageBox.Show("Welcome Admin!", "Verified User ❤️", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Navigate to Doctor Dashboard (example)
                    MainFrame.Navigate(new Doctor_Dashboard()); // Assuming you have a MainFrame for navigation
                }
                else
                {
                    // Show an error message if the credentials are incorrect
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Show a warning if the password is not numeric
                MessageBox.Show("Password must be a numeric value", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPass.Clear();
                txtUser.Clear();
                txtUser.Focus();
            }
        }

        // Cancel button click handler
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Go back to MainWindow
            this.Close();  // Closes the Reset Password window and returns to the previous one
        }
    }
}