using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IRepository<Doctor> _DoctorRepository;
        private readonly IRepository<Nurse> _NurseRepository;
        private readonly IRepository<Reset_Pass_Log> _Reset_Pass_Log;

        public Reset_Password()
        {
            InitializeComponent();
            var services = ((App)Application.Current)._serviceProvider;
            _DoctorRepository = services.GetService<IRepository<Doctor>>() ?? throw new InvalidOperationException("Data helper service is not available"); ;
            _NurseRepository = services.GetService<IRepository<Nurse>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _Reset_Pass_Log = services.GetService<IRepository<Reset_Pass_Log>>() ?? throw new InvalidOperationException("Data helper service is not available");
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string pass = pass_txt.Password;
            string user = user_txt.Text;
            string adminpass = txtAdmin.Password;
            int iddoc = int.Parse(ID_txt.Text);
            int idnurse = int.Parse(ID_txt.Text);
            int adminid = int.Parse(ID_txt.Text);

            Reset_Pass_Log reset_Pass_Logvar = await _Reset_Pass_Log.GetByIdAsync(adminid);
            Doctor docvar = await _DoctorRepository.GetByIdAsync(iddoc);
            Nurse Nursevar = await _NurseRepository.GetByIdAsync(idnurse);

            if (reset_Pass_Logvar != null && reset_Pass_Logvar.Admin_Pass == adminpass && reset_Pass_Logvar.ID == adminid)
            {
                if (Nursevar != null && user == Nursevar.user_name)
                {
                    Nursevar.password = pass;
                    _NurseRepository.UpdateAsync(Nursevar, idnurse);
                    MessageBox.Show("Updated", "Confirm", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }
                else if (docvar != null && user == docvar.User_Name)
                {
                    docvar.Pass = pass;
                    _DoctorRepository.UpdateAsync(docvar, iddoc);
                    MessageBox.Show("Updated", "Confirm", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Invalid User Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid Admin Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                pass_txt.Clear();
                user_txt.Clear();
                ID_txt.Clear();
                txtAdmin.Clear();
                user_txt.Focus();

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