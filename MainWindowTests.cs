using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using E_Vita.Views;
using System.Windows;

namespace E_Vita.Tests
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void MainWindow_ShouldInitializeComponents()
        {
            // Arrange
            var mainWindow = new MainWindow();

            // Act
            mainWindow.Show();

            // Assert
            Assert.IsNotNull(mainWindow);
            Assert.AreEqual(WindowState.Maximized, mainWindow.WindowState);
            Assert.AreEqual("E-Vita", mainWindow.Title);
        }

        [TestMethod]
        public void LoginButton_Click_ShouldTriggerEvent()
        {
            // Arrange
            var mainWindow = new MainWindow();
            var loginButton = mainWindow.FindName("btnLogin") as Button;

            bool eventTriggered = false;
            loginButton.Click += (s, e) => eventTriggered = true;

            // Act
            loginButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            // Assert
            Assert.IsTrue(eventTriggered);
        }

        [TestMethod]
        public void ResetButton_Click_ShouldTriggerEvent()
        {
            // Arrange
            var mainWindow = new MainWindow();
            var resetButton = mainWindow.FindName("Reset") as Button;

            bool eventTriggered = false;
            resetButton.Click += (s, e) => eventTriggered = true;

            // Act
            resetButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            // Assert
            Assert.IsTrue(eventTriggered);
        }
    }
}
