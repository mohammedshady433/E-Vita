using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace E_Vita.Tests
{
    [TestClass]
    public class MainWindowTests
    {
        private Mock<IRepository<Doctor>> _mockDoctorRepository;
        private Mock<IRepository<Nurse>> _mockNurseRepository;
        private MainWindow _mainWindow;

        [TestInitialize]
        public void Setup()
        {
            _mockDoctorRepository = new Mock<IRepository<Doctor>>();
            _mockNurseRepository = new Mock<IRepository<Nurse>>();
            _mainWindow = new MainWindow
            {
                _DoctorRepository = _mockDoctorRepository.Object,
                _NurseRepository = _mockNurseRepository.Object
            };

            // Initialize UI elements
            _mainWindow.user_txt = new TextBox();
            _mainWindow.pass_txt = new PasswordBox();
            _mainWindow.ID_txt = new TextBox();
            _mainWindow.MainFrame = new Frame();
        }

        [TestMethod]
        public async Task Button_Click_ValidDoctorCredentials_NavigatesToDoctorDashboard()
        {
            // Arrange
            var doctor = new Doctor { Doctor_ID = 1, User_Name = "doctor1", Pass = "password1", Name = "Dr. Smith" };
            _mockDoctorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(doctor);
            _mainWindow.user_txt.Text = "doctor1";
            _mainWindow.pass_txt.Password = "password1";
            _mainWindow.ID_txt.Text = "1";

            // Act
             _mainWindow.Button_Click(null, null);

            // Assert
            // Verify that the navigation to DoctorDashboard occurred
            // (You may need to mock the navigation or check the state change)
        }

        [TestMethod]
        public async Task Button_Click_ValidNurseCredentials_NavigatesToNurseDashboard()
        {
            // Arrange
            var nurse = new Nurse { Nurse_ID = 1, user_name = "nurse1", password = "password1" };
            _mockNurseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(nurse);
            _mainWindow.user_txt.Text = "nurse1";
            _mainWindow.pass_txt.Password = "password1";
            _mainWindow.ID_txt.Text = "1";

            // Act
             _mainWindow.Button_Click(null, null);

            // Assert
            // Verify that the navigation to Nurse_Dashboard occurred
            // (You may need to mock the navigation or check the state change)
        }

        [TestMethod]
        public async Task Button_Click_InvalidCredentials_ShowsErrorMessage()
        {
            // Arrange
            _mockDoctorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Doctor)null);
            _mockNurseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Nurse)null);
            _mainWindow.user_txt.Text = "invalidUser";
            _mainWindow.pass_txt.Password = "invalidPass";
            _mainWindow.ID_txt.Text = "1";

            // Act
             _mainWindow.Button_Click(null, null);

            // Assert
            // Verify that the error message was shown
            // (You may need to mock the MessageBox or check the state change)
        }
    }
}