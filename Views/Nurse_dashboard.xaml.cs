using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_Vita
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Nurse_Dashboard : Page
    {
        private DateTime currentDate;
        private readonly IRepository<Appointment> _Appointment;

        public Nurse_Dashboard()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            PopulateYearAndMonthSelectors();
            GenerateCalendar(currentDate);
            var services = ((App)Application.Current)._serviceProvider;
            _Appointment = services.GetService<IRepository<Appointment>>() ?? throw new InvalidOperationException("Data helper service is not available");
            LoadAppointmentsFortoday();
        }
        public async void LoadAppointmentsFortoday()
        {
            var today = DateTime.Today; // Get today's date without time
            var todayAppointments = await _Appointment.GetAllAsync();

            //Filter appointments where the Date matches today's date
            var filteredAppointments = todayAppointments
                .Where(a => a.Date.Date == today)
                .ToList();

            ScheduleDataGrid.ItemsSource = todayAppointments;
        }
        public async void LoadAppointmentsForDate(DateTime specificDate)
        {
            var specificDateAppointments = await _Appointment.GetAllAsync();

            // Filter appointments where the Date matches the specific date
            var filteredAppointments = specificDateAppointments
                .Where(a => a.Date.Date == specificDate.Date)
                .ToList();

            ScheduleDataGrid.ItemsSource = filteredAppointments;
        }

        private void PopulateYearAndMonthSelectors()
        {
            // Populate years (e.g., +/- 10 years from the current year)
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear - 10; year <= currentYear + 10; year++)
            {
                YearSelector.Items.Add(year);
            }
            YearSelector.SelectedItem = currentDate.Year;

            // Populate months
            for (int month = 1; month <= 12; month++)
            {
                MonthSelector.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month));
            }
            MonthSelector.SelectedIndex = currentDate.Month - 1;
        }

        private void GenerateCalendar(DateTime date)
        {
            CalendarGrid.Children.Clear();

            // Add day names (Sunday-Saturday)
            string[] dayNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
            foreach (string dayName in dayNames)
            {
                CalendarGrid.Children.Add(new TextBlock
                {
                    Text = dayName,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(5),
                    Foreground = new BrushConverter().ConvertFrom("#891739") as Brush
                });
            }

            // Get the first day of the month
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            int startDayOffset = (int)firstDayOfMonth.DayOfWeek;

            // Add empty cells for days before the first of the month
            for (int i = 0; i < startDayOffset; i++)
            {
                CalendarGrid.Children.Add(new TextBlock());
            }

            // Add buttons for each day in the month
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDay = new DateTime(date.Year, date.Month, day);
                Button dayButton = new Button
                {
                    Content = day.ToString(),
                    Margin = new Thickness(5),
                    Background = Brushes.White,
                    BorderBrush = Brushes.Gray,
                    Foreground = Brushes.Black,
                    Tag = currentDay
                };

                // Highlight today's date
                if (currentDay.Date == DateTime.Now.Date)
                {
                    dayButton.Background = (Brush)new BrushConverter().ConvertFrom("#891739") ?? Brushes.Transparent;
                    dayButton.FontWeight = FontWeights.Bold;
                }

                dayButton.Click += DayButton_Click;
                CalendarGrid.Children.Add(dayButton);
            }
        }

        private void PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            YearSelector.SelectedItem = currentDate.Year;
            MonthSelector.SelectedIndex = currentDate.Month - 1;
            GenerateCalendar(currentDate);
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            YearSelector.SelectedItem = currentDate.Year;
            MonthSelector.SelectedIndex = currentDate.Month - 1;
            GenerateCalendar(currentDate);
        }

        private void YearSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (YearSelector.SelectedItem != null && MonthSelector.SelectedIndex >= 0)
            {
                currentDate = new DateTime((int)YearSelector.SelectedItem, MonthSelector.SelectedIndex + 1, 1);
                GenerateCalendar(currentDate);
            }
        }

        private void MonthSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (YearSelector.SelectedItem != null && MonthSelector.SelectedIndex >= 0)
            {
                currentDate = new DateTime((int)YearSelector.SelectedItem, MonthSelector.SelectedIndex + 1, 1);
                GenerateCalendar(currentDate);
            }
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is DateTime selectedDate)
            {
                //MessageBox.Show($"Clicked on {selectedDate:MMMM dd, yyyy}");
                LoadAppointmentsForDate(selectedDate);

            }
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Add_Patient());
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            BookAppointmentWindow bookAppointmentWindow = new BookAppointmentWindow();
            bookAppointmentWindow.ShowDialog();
        }

        private void ScheduleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Schedule item selected.");
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to login page
            var mainWindow = (MainWindow)Window.GetWindow(this);
            if (mainWindow != null)
            {
                // Clear any stored credentials or session data
                mainWindow.pass_txt.Clear();
                mainWindow.user_txt.Clear();
                mainWindow.ID_txt.Clear();
                
                // Clear the navigation history and return to the main content
                while (mainWindow.MainFrame.CanGoBack)
                {
                    mainWindow.MainFrame.RemoveBackEntry();
                }
                mainWindow.MainFrame.Content = null;
            }
        }
    }
}
