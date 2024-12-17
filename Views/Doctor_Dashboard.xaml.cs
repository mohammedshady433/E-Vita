using E_Vita.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace E_Vita
{
    public partial class DoctorDashboard : Page
    {
        private DateTime currentDate;

        public DoctorDashboard()
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
                    Margin = new Thickness(5),
                    Foreground = new BrushConverter().ConvertFromString("#0F4C75") as Brush
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
                    Background = Brushes.Black,
                    BorderBrush = Brushes.Gray,
                    Foreground = Brushes.White,
                    Tag = currentDay,
                    Width = 90,
                    Height = 71
                };

                // Highlight today's date
                if (currentDay.Date == DateTime.Now.Date)
                {
                    dayButton.Background = (Brush)new BrushConverter().ConvertFromString("#0F4C75") ?? Brushes.Transparent;
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
                MessageBox.Show($"Clicked on {selectedDate:MMMM dd, yyyy}");
            }
        }

        private void ScheduleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Schedule item selected.");
        }

        private void ViewPatientsData_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Patient_Data());
        }

        private void Prescriptions_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Prescriptions button clicked.");
        }

        private void LabResults_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Lab Tests Information button clicked.");
            this.NavigationService.Navigate(new LabTestsWindow());
        }

        private void Images_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Images button clicked.");
        }

        private void Finance_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Finance button clicked.");
        }

        private void PatientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Patient_Data selected button clicked.");
            this.NavigationService.Navigate(new Patient_Data());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Finance());

        }

        private void Medications_Click(object sender, RoutedEventArgs e)
        {
            MedicationsWindow medWindow = new MedicationsWindow();
            medWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Patient_info());

        }
    }
}