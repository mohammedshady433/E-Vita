using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient; // For connecting to MySQL
using System.Data;           // For using DataTable

namespace E_Vita
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=localhost;Database=hotel_lab5;User ID=root;Password=Moh@10042004;SslMode=none;";

        public MainWindow()
        {
            InitializeComponent();
        }

        // Test the connection to the database
        private void TestConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Connection successful!", "Database Connection", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            int nursePassword = 123;
            string nurseUsername = "Mohammed";
            int doctorPassword = 123;
            string doctorUsername = "doctor";

            // Try parsing the input password to an integer
            if (int.TryParse(txtPass.Password, out int password))
            {
                // Validate nurse credentials
                if (password == nursePassword && txtUser.Text == nurseUsername)
                {
                    MessageBox.Show("Welcome Nurse Mohammed!", "Verified User ❤️", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainFrame.Navigate(new Nurse_Dashboard());
                }
                // Validate doctor credentials
                else if (password == doctorPassword && txtUser.Text == doctorUsername)
                {
                    MessageBox.Show("Welcome Doctor!", "Verified User ❤️", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainFrame.Navigate(new Doctor_Dashboard());
                }
                else
                {
                    // Handle invalid credentials
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Handle invalid password input
                MessageBox.Show("Password must be a numeric value", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPass.Clear();
                txtUser.Clear();
                txtUser.Focus();
            }
        }

        // Reset password navigation
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Reset_Password());
        }
    }
}
