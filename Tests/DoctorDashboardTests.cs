using E_Vita;
using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xunit;
//using static System.Net.Mime.MediaTypeNames;

public class DoctorDashboardTests
{
    private readonly Mock<IRepository<Appointment>> _mockAppointmentRepository;
    private readonly DoctorDashboard _doctorDashboard;

    public DoctorDashboardTests()
    {
        _mockAppointmentRepository = new Mock<IRepository<Appointment>>();
        var serviceProvider = new Mock<IServiceProvider>();
        serviceProvider.Setup(x => x.GetService(typeof(IRepository<Appointment>)))
                       .Returns(_mockAppointmentRepository.Object);
        var app = new Mock<Application>();
        var appType = typeof(Application);
        var field = appType.GetField("_serviceProvider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(app.Object, serviceProvider.Object);
        var currentProperty = appType.GetProperty("Current", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        currentProperty.SetValue(null, app.Object);

        _doctorDashboard = new DoctorDashboard();
    }

    [Fact]
    public async Task LoadAppointmentsForToday_ShouldLoadAppointmentsForToday()
    {
        // Arrange
        var today = DateTime.Today;
        var appointments = new List<Appointment>
        {
            new Appointment { Date = today, Patient_appointment = new Patient { name = "John Doe" } },
            new Appointment { Date = today.AddDays(1), Patient_appointment = new Patient { name = "Jane Doe" } }
        };
        _mockAppointmentRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(appointments);

        // Act
        _doctorDashboard.LoadAppointmentsForToday();
        await Task.Delay(100); // Wait for async method to complete

        // Assert
        Assert.Single(_doctorDashboard.AppointmentsForToday);
        Assert.Equal("John Doe", _doctorDashboard.AppointmentsForToday.First().Patient_appointment.name);
    }

    [Fact]
    public async Task LoadAppointmentsForDate_ShouldLoadAppointmentsForSpecificDate()
    {
        // Arrange
        var specificDate = new DateTime(2023, 10, 10);
        var appointments = new List<Appointment>
        {
            new Appointment { Date = specificDate, Patient_appointment = new Patient { name = "John Doe" } },
            new Appointment { Date = specificDate.AddDays(1), Patient_appointment = new Patient { name = "Jane Doe" } }
        };
        _mockAppointmentRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(appointments);

        // Act
        _doctorDashboard.LoadAppointmentsForDate(specificDate);
        await Task.Delay(100); // Wait for async method to complete

        // Assert
        Assert.Single(_doctorDashboard.AppointmentsForToday);
        Assert.Equal("John Doe", _doctorDashboard.AppointmentsForToday.First().Patient_appointment.name);
    }

    [Fact]
    public void GenerateCalendar_ShouldGenerateCalendarForGivenDate()
    {
        // Arrange
        var date = new DateTime(2023, 10, 1);

        // Act
        _doctorDashboard.GenerateCalendar(date);

        // Assert
        Assert.NotEmpty(_doctorDashboard.CalendarGrid.Children);
    }

    [Fact]
    public void PopulateYearAndMonthSelectors_ShouldPopulateSelectors()
    {
        // Act
        _doctorDashboard.PopulateYearAndMonthSelectors();

        // Assert
        Assert.NotEmpty(_doctorDashboard.YearSelector.Items);
        Assert.NotEmpty(_doctorDashboard.MonthSelector.Items);
    }
}
