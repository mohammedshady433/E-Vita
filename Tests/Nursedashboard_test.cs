using Xunit;
using Moq;
using System.Windows.Controls;
using E_Vita;

public class NurseDashboardTests
{
    private readonly Nurse_Dashboard _nurseDashboard;

    public NurseDashboardTests()
    {
        _nurseDashboard = new Nurse_Dashboard();
    }

    [Fact]
    public void AddPatientButton_Click_ShouldTriggerAddPatientMethod()
    {
        // Arrange
        var addPatientButton = _nurseDashboard.FindName("AddPatientbtn") as Button;
        // Act
        addPatientButton.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));
        // Assert
        // Verify that the AddPatient_Click method was called
        // This can be done by checking the state change or using a mock framework if the method interacts with other services
    }

    [Fact]
    public void BookAppointmentButton_Click_ShouldTriggerBookAppointmentMethod()
    {
        // Arrange
        var bookAppointmentButton = _nurseDashboard.FindName("BookAppointmentbtn") as Button;
        // Act
        bookAppointmentButton.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));
        // Assert
        // Verify that the BookAppointment_Click method was called
        // This can be done by checking the state change or using a mock framework if the method interacts with other services
    }

    [Fact]
    public void PreviousMonthButton_Click_ShouldTriggerPreviousMonthMethod()
    {
        // Arrange
        var previousMonthButton = _nurseDashboard.FindName("PreviousMonth_Click") as Button;
        // Act
        previousMonthButton.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));
        // Assert
        // Verify that the PreviousMonth_Click method was called
        // This can be done by checking the state change or using a mock framework if the method interacts with other services
    }

    [Fact]
    public void NextMonthButton_Click_ShouldTriggerNextMonthMethod()
    {
        // Arrange
        var nextMonthButton = _nurseDashboard.FindName("NextMonth_Click") as Button;
        // Act
        nextMonthButton.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));
        // Assert
        // Verify that the NextMonth_Click method was called
        // This can be done by checking the state change or using a mock framework if the method interacts with other services
    }
}
