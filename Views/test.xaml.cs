using System;
using System.Collections.Generic;
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
    /// Interaction logic for Nurse_Dashboard.xaml
    /// </summary>
    public partial class test : Page
    {
        private DateTime _currentDate = DateTime.Now;

        public test()
        {
            InitializeComponent();
            UpdateCalendar(); // Populate the calendar on startup
        }

        private void UpdateCalendar()
        {
            CalendarGrid.Children.Clear();
            string[] headers = { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
            foreach (string header in headers)
            {
                CalendarGrid.Children.Add(new TextBlock
                {
                    Text = header,
                    FontWeight = FontWeights.Bold,
                    FontSize = 16,
                    Foreground = new System.Windows.Media.BrushConverter().ConvertFrom("#0F4C75") as System.Windows.Media.Brush,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                });
            }

            var firstDayOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);
            int startDayOffset = (int)firstDayOfMonth.DayOfWeek;

            for (int i = 0; i < startDayOffset; i++)
            {
                CalendarGrid.Children.Add(new TextBlock());
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(_currentDate.Year, _currentDate.Month, day);
                Button dayButton = new Button
                {
                    Content = day.ToString(),
                    Background = System.Windows.Media.Brushes.White,
                    Foreground = System.Windows.Media.Brushes.Black,
                    Margin = new Thickness(2),
                    Tag = date
                };

                if (date.Day == _currentDate.Day)
                {
                    dayButton.Background = (System.Windows.Media.Brush?)new System.Windows.Media.BrushConverter().ConvertFrom("#BBE1FA") ?? System.Windows.Media.Brushes.Transparent;
                }

                dayButton.Click += DayButton_Click;
                CalendarGrid.Children.Add(dayButton);
            }

            MonthYearLabel.Text = _currentDate.ToString("MMMM yyyy");
        }

        private void PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(-1);
            UpdateCalendar();
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(1);
            UpdateCalendar();
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is DateTime selectedDate)
            {
                MessageBox.Show($"Clicked on {selectedDate:MMMM dd, yyyy}");
            }
        }

        private void ScheduleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Add_Patient());
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            // Call the constructor with the correct parameters
            BookAppointmentWindow bookAppointmentWindow = new BookAppointmentWindow();
            bookAppointmentWindow.ShowDialog();
        }
    }
}
