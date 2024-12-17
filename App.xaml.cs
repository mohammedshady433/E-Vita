using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion;
using Syncfusion.Licensing;
using Microsoft.Extensions.Hosting;
using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using E_Vita.Views;
using LiveCharts.Wpf;
namespace E_Vita
{
    public partial class App : Application
    {
        //public static IServiceProvider ServiceProvider { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            // Set up Dependency Injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Register the Syncfusion license key
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cWWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFiWH1WcnVVQmNYUk1wWw==");
        }

        // The OnStartup method should only resolve and show the MainWindow
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Get the MainWindow from DI container and show it
            // Resolve MainWindow using the DI container
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            // Show the MainWindow

            mainWindow.Show();

        }
        private void ConfigureServices(IServiceCollection services)
        {
            // Register DbContext, services, and views
            //services.AddScoped<MainWindow>();
            //services.AddScoped<DoctorDashboard>();
            //services.AddScoped<Nurse_Dashboard>();
            //services.AddScoped<Appointments>();
            //services.AddScoped<Add_Patient>();
            //services.AddScoped<BookAppointmentWindow>();
            //services.AddScoped<Finance>();
            //services.AddScoped<Patient_Data>();
            //services.AddScoped<Patient_info>();
            //services.AddScoped<Reset_Password>();
            services.AddDbContext<ApplicationDbContext>();

            //repositories injection
            services.AddScoped<IRepository<Appointment>,AppointmentRepo>();
            services.AddScoped<IRepository<Doctor>, DoctorRepo>();
            services.AddScoped<IRepository<Nurse>, NurseRepo>();
            services.AddScoped<IRepository<Patient>, PatientRepo>();
            services.AddScoped<IRepository<Prescription>, PrescriptionRepo>();

        }

    }
}