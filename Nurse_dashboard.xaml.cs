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
    /// Interaction logic for Nurse_Dashboard.xaml
    /// </summary>
    public partial class Nurse_Dashboard : Page
    {
        public  Nurse_Dashboard()

        {
            InitializeComponent();
        }

        private void add_Patient(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Add_Patient());
        }
    }
}
