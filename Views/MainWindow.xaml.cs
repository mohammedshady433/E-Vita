using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace E_Vita
{
    public partial class MainWindow : Window
    {
        private readonly IRepository<Doctor> _DoctorRepository;
        private readonly IRepository<Nurse> _NurseRepository;
        public MainWindow()
        {
            InitializeComponent();
            var services = ((App)Application.Current)._serviceProvider;
            _DoctorRepository = services.GetService<IRepository<Doctor>>() ?? throw new InvalidOperationException("Data helper service is not available");
            _NurseRepository = services.GetService<IRepository<Nurse>>() ?? throw new InvalidOperationException("Data helper service is not available");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoForward)
            {
                MainFrame.GoForward();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string pass = pass_txt.Password;
            string user = user_txt.Text;
            int iddoc = int.Parse(ID_txt.Text);
            int idnurse = int.Parse(ID_txt.Text);

            Doctor docvar = await _DoctorRepository.GetByIdAsync(iddoc);
            Nurse Nursevar = await _NurseRepository.GetByIdAsync(idnurse);
            
            // Try parsing the input password to an integer
            
                // Validate nurse credentials
                if (Nursevar != null && Nursevar.password == pass && user == Nursevar.user_name)
                {
                    MessageBox.Show($"Welcome Nurse {Nursevar.user_name}", "Verified User ❤️", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainFrame.Navigate(new Nurse_Dashboard());
                }
                // Validate doctor credentials
                else if (docvar != null && docvar.Pass == pass && user == docvar.User_Name)
                {
                    MessageBox.Show($"Welcome Doctor! {docvar.Name}", "Verified User ❤️", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainFrame.Navigate(new DoctorDashboard());
                }
                else
                {
                    // Handle invalid credentials
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    pass_txt.Clear();
                    user_txt.Clear();
                    ID_txt.Clear();
                    user_txt.Focus();
                }
            
          
        }

        // Reset password navigation
        private void Reset_Pass(object sender, RoutedEventArgs e)
        {
            Reset_Password Resetpass = new Reset_Password();
            Resetpass.ShowDialog();
        }


    }
}



//trash code 
//private void TestConnection()
//{
//    try
//    {
//        using (MySqlConnection conn = new MySqlConnection(connectionString))
//        {
//            conn.Open();
//            MessageBox.Show("Connection successful!", "Database Connection", MessageBoxButton.OK, MessageBoxImage.Information);
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
//    }
//}

//private void MainFrame_Navigated(object sender, NavigationEventArgs e)
//{

//}



//this was written in the contructor
//private string connectionString = "Server=localhost;Database=hotel_lab5;User ID=root;Password=Moh@10042004;SslMode=none;";
//private readonly IRepository<Doctor> doctor_Db;

//private IRepository<Models.Doctor> doctordb;



//public static MainWindow CreateInstance(IRepository<Doctor> doctorRepository, IRepository<Nurse> nurseRepository)
//{
//    return new MainWindow(doctorRepository, nurseRepository);
//}
//private void LoadData() {
//    // Load data from the database
//    var patients = _context.patient_Datas.ToList();
//    var doctors = _context.Doctors.ToList();
//    var medicalRecords = _context.Medical_Records.ToList();
//    var patientDoctorNurses = _context.patient_Doctor_Nurses.ToList();
//    var prescriptions = _context.Prescriptions.ToList();
//    var resetPassLogs = _context.Reset_Pass_Logs.ToList();
//    var nurses = _context.Nurses.ToList();
//    var appointments = _context.Appointments_DB.ToList();
//}