using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion;
using Syncfusion.Licensing;
using Microsoft.Extensions.Hosting;
using E_Vita.Interfaces.Repository;
using E_Vita.Models;
namespace E_Vita
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public static IServiceProvider ServiceProvider { get; private set; }
        public static IHost AppHost { get; private set; }
        public App()
        {
            // Register the Syncfusion license key
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cWWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFiWH1WcnVVQmNYUk1wWw==");
            AppHost= Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<IRepository<Doctor>, DoctorRepo>();
                    services.AddTransient<IRepository<Nurse>, NurseRepo>();
                    services.AddTransient<IRepository<Patient>, PatientRepo>();
                    services.AddTransient<IRepository<Prescription>, PrescriptionRepo>();
                    services.AddTransient<IRepository<Appointment>, AppointmentRepo>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            // Resolve and show the Main Window
            var startupform = AppHost.Services.GetRequiredService<MainWindow>();
            startupform.Show();

            base.OnStartup(e);

            //    // Initialize the dependency injection container
            //    ServiceProvider = Startup.ConfigureServices();
                
        }
        protected override async void OnExit(ExitEventArgs e)
        {
           
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}