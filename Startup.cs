using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using E_Vita;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using E_Vita.Interfaces.Repository;  // Replace with your namespace
using E_Vita.Models;                 // Replace with your namespace

namespace E_Vita
{
    public class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register your DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=localhost;Database=E-Vita;User ID=root;Password=Moh@10042004;SslMode=Preferred;"));

            services.AddTransient<MainWindow>();

            // Register Repositories
            services.AddScoped<IRepository<Appointment>, AppointmentRepo>();
            services.AddScoped<IRepository<Doctor>, DoctorRepo>();
            services.AddScoped<IRepository<Nurse>, NurseRepo>();
            services.AddScoped<IRepository<Patient>, PatientRepo>();
            services.AddScoped<IRepository<Prescription>, PrescriptionRepo>();

            // Return the service provider
            return services.BuildServiceProvider();
        }
    }
}
