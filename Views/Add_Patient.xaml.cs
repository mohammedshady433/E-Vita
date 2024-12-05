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
    /// Interaction logic for Add_Patient.xaml
    /// </summary>
    public partial class Add_Patient : Page
    {
        public Add_Patient()
        {
            InitializeComponent();
        }
        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Appointments());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FinancePage());
        }
    }
}
