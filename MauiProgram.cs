using E_Vita.Interfaces.Repository;
using E_Vita.Models;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Qatar2022.ttf", "Qatar2022");
            });

        // Register your services
        builder.Services.AddSingleton<IRepository<Doctor>, DoctorRepository>();
        builder.Services.AddSingleton<IRepository<Nurse>, NurseRepository>();
        builder.Services.AddSingleton<IRepository<Appointment>, AppointmentRepository>();

        // Register your pages
        builder.Services.AddTransient<DoctorDashboard>();

        return builder.Build();
    }
} 