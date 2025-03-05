using E_Vita.Interfaces.Repository;
using E_Vita.Models;

namespace E_Vita.Mobile.Views
{
    public partial class DoctorDashboard : ContentPage
    {
        private readonly IRepository<Appointment> _appointmentRepository;
        private DateTime currentDate;

        public DoctorDashboard(IRepository<Appointment> appointmentRepository)
        {
            InitializeComponent();
            _appointmentRepository = appointmentRepository;
            currentDate = DateTime.Now;
            
            // Initialize calendar and load appointments
            GenerateCalendar(currentDate);
            LoadAppointmentsForToday();
        }

        private async void LoadAppointmentsForToday()
        {
            try
            {
                var today = DateTime.Today;
                var appointments = await _appointmentRepository.GetAllAsync();
                var todayAppointments = appointments.Where(a => a.Date.Date == today).ToList();
                AppointmentsList.ItemsSource = todayAppointments;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading appointments: {ex.Message}", "OK");
            }
        }

        private void GenerateCalendar(DateTime date)
        {
            // Implement calendar generation similar to WPF version
            // Adapted for MAUI's layout system
        }

        private async void ViewPatientsData_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PatientDataPage());
        }

        private async void LabResults_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LabResultsPage());
        }

        private async void Finance_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FinancePage());
        }

        private async void LogOut_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (answer)
            {
                await Navigation.PopToRootAsync();
            }
        }
    }
} 