using E_Vita.Interfaces.Repository;
using E_Vita.Models;
using E_Vita.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace E_Vita
{
    public partial class DoctorDashboard : Page
    {
        private DateTime currentDate;
        private readonly IRepository<Appointment> _Appointment;
        public ObservableCollection<Appointment> AppointmentsForToday { get; set; }

        public DoctorDashboard()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            PopulateYearAndMonthSelectors();
            GenerateCalendar(currentDate);
            var services = ((App)Application.Current)._serviceProvider;
            _Appointment = services.GetService<IRepository<Appointment>>() ?? throw new InvalidOperationException("Data helper service is not available");

            AppointmentsForToday = new ObservableCollection<Appointment>();
            LoadAppointmentsForToday();
        }


        public async void LoadAppointmentsForToday()
        {
            try
            {
                var today = DateTime.Today; // Get today's date without time
                var todayAppointments = await _Appointment.GetAllAsync();

                // Filter appointments where the Date matches today's date
                var filteredAppointments = todayAppointments
                    .Where(a => a.Date.Date == today)
                    .ToList();

                //// Update the ObservableCollection
                AppointmentsForToday.Clear();
                foreach (var appointment in filteredAppointments)
                {
                    AppointmentsForToday.Add(appointment);
                }
                listviewdoc.ItemsSource = AppointmentsForToday;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}");
            }
        }

        public async void LoadAppointmentsForDate(DateTime specificDate)
        {
            try
            {
                var specificDateAppointments = await _Appointment.GetAllAsync();

                // Filter appointments where the Date matches the specific date
                var filteredAppointments = specificDateAppointments
                    .Where(a => a.Date.Date == specificDate.Date)
                    .ToList();

                //// Update the ObservableCollection
                AppointmentsForToday.Clear();
                foreach (var appointment in filteredAppointments)
                {
                    AppointmentsForToday.Add(appointment);
                }
                listviewdoc.ItemsSource = AppointmentsForToday;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}");
            }
        }

        public void PopulateYearAndMonthSelectors()
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

        public void GenerateCalendar(DateTime date)
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
                LoadAppointmentsForDate(selectedDate);
                //MessageBox.Show($"Clicked on {selectedDate:MMMM dd, yyyy}");
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
            this.NavigationService.Navigate(new Labimages());
        }

        private void Images_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Labimages());
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

        private void patient_treat_assess_click(object sender, RoutedEventArgs e)
        {
            if (listviewdoc.SelectedItem is Appointment selectedAppointment)
            {
                var patient = selectedAppointment.Patient_appointment;
                this.NavigationService.Navigate(new Patient_info(patient));
            }
            else
            {
                return;     
            }
        }
    }
}