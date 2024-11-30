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

namespace E_Vita
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int pass = 123;
            string name = "Mohammed";
            if (int.TryParse(pass_txt.Password, out int passowrd))
            {
                if (passowrd == pass && name == user_txt.Text)
                {
                    MessageBox.Show("Verified user", "Welcome❤️", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Content = new Nurse_Dashboard();
                }
                else
                {
                    MessageBox.Show("Declined user", "Get out!", MessageBoxButton.OK, MessageBoxImage.Error);
                    pass_txt.Clear();
                    user_txt.Clear();
                    user_txt.Focus();
                }
            }
            else 
            {
                MessageBox.Show("Please enter a valid format", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                pass_txt.Clear();
                user_txt.Clear();
                user_txt.Focus();
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Reset_Pass(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Reset_Password());
        }
    }
}