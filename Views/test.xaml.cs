using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace E_Vita
{
    public partial class test : Page
    {
        private DateTime currentDate;

        public test()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            PopulateYearAndMonthSelectors();
            GenerateCalendar(currentDate);
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
            Margin = new Thickness(5)
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
        Button dayButton = new Button
        {
            Content = day.ToString(),
            Margin = new Thickness(5),
            Background = System.Windows.Media.Brushes.White,
            BorderBrush = System.Windows.Media.Brushes.Gray,
            Foreground = System.Windows.Media.Brushes.Black
        };

        // Highlight today's date
        if (date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month && day == DateTime.Now.Day)
        {
            dayButton.Background = System.Windows.Media.Brushes.LightBlue;
            dayButton.FontWeight = FontWeights.Bold;
        }

        dayButton.Click += (s, e) => OnDayButtonClick(date.Year, date.Month, day);
        CalendarGrid.Children.Add(dayButton);
    }
}

        private void OnDayButtonClick(int year, int month, int day)
        {
            MessageBox.Show($"You selected {new DateTime(year, month, day).ToShortDateString()}.", "Date Selected");
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

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add Patient button clicked.");
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Book Appointment button clicked.");
        }

        private void ScheduleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change in the schedule list
            MessageBox.Show("Schedule item selected.");
        }
    }
}
