using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using E_Vita.Views;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Licensing;
using System.Windows;
namespace E_Vita
{
    public partial class App : Application
    {
        //public static IServiceProvider ServiceProvider { get; private set; }
        public IServiceProvider _serviceProvider;


        // The OnStartup method should only resolve and show the MainWindow
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Register the Syncfusion license key
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cWWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFiWH1WcnVVQmNYUk1wWw==");

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            // Show the MainWindow
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>();

            //repositories injection
            services.AddScoped<IRepository<Appointment>, AppointmentRepo>();
            services.AddScoped<IRepository<Doctor>, DoctorRepo>();
            services.AddScoped<IRepository<Nurse>, NurseRepo>();
            services.AddScoped<IRepository<Patient>, PatientRepo>();
            services.AddScoped<IRepository<Prescription>, PrescriptionRepo>();
            services.AddScoped<IRepository<Reset_Pass_Log>, Reset_PassRepo>();
            // Register DbContext, services, and views
            services.AddSingleton<MainWindow>();
            services.AddScoped<DoctorDashboard>();
            services.AddScoped<Nurse_Dashboard>();
            services.AddScoped<Appointments>();
            services.AddScoped<Add_Patient>();
            services.AddScoped<BookAppointmentWindow>();
            services.AddScoped<Finance>();
            services.AddScoped<Patient_Data>();
            services.AddScoped<Patient_info>();
            services.AddScoped<Reset_Password>();

        }

    }
}