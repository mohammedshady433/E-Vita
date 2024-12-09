using System;
using System.Windows;
using System.Windows.Controls;

namespace E_Vita
{
    public partial class Nurse_Dashboard : Window
    {
        private DateTime _currentDate = DateTime.Now;

        public Nurse_Dashboard()
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

                if (date.Day == 16 || date.Day == 26)
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
            var addPatientPage = new AddPatientWindow();
            this.Content = addPatientPage;
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            // Call the constructor with the correct parameters
            BookAppointmentWindow bookAppointmentWindow = new BookAppointmentWindow();
            bookAppointmentWindow.ShowDialog();
        }
    }
}
