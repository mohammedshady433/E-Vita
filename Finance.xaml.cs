using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace E_Vita
{
    public partial class Finance : Page
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public ChartValues<double> RevenueValues { get; set; }
        public ChartValues<double> ExpenseValues { get; set; }
        public ChartValues<double> ProfitValues { get; set; }
        public ObservableCollection<string> MonthLabels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public Finance()
        {
            InitializeComponent();

            this.DataContext = this;

            // Initialize Transactions collection with sample data
            Transactions = new ObservableCollection<Transaction>
            {
                new Transaction { Date = "2024-11-01", Description = "Sale of Product A", Amount = 1500.00, Type = "Income" },
                new Transaction { Date = "2024-11-05", Description = "Purchase of Supplies", Amount = 200.00, Type = "Expense" },
                new Transaction { Date = "2024-11-10", Description = "Sale of Service B", Amount = 3000.00, Type = "Income" },
                new Transaction { Date = "2024-11-15", Description = "Purchase of Equipment", Amount = 500.00, Type = "Expense" }
            };

            // Check if Transactions data is correctly populated
            Debug.WriteLine($"Transaction Count: {Transactions.Count}"); // Debug log

            // Bind the Transactions collection to the DataGrid
            TransactionsDataGrid.ItemsSource = Transactions;

            // Debugging DataGrid binding
            Debug.WriteLine($"DataGrid ItemsSource is set.");

            // Set up data for the charts
            RevenueValues = new ChartValues<double> { 17000, 60000, 8000, 8000};
            ExpenseValues = new ChartValues<double> { 15000, 25000, 3000, 3500};
            ProfitValues = new ChartValues<double> { 2000, 35000, 5000, 4500};

            // Define month labels and Y-axis formatter
            MonthLabels = new ObservableCollection<string> { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => $"${value:N0}";

            // Bind data to charts
            RevenueExpensesChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                     Title = "Revenue",
                    Values = RevenueValues,
                    Stroke = System.Windows.Media.Brushes.Orchid,
                    Fill = System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                     Title = "Expenses",
                    Values = ExpenseValues,
                    Stroke = System.Windows.Media.Brushes.OrangeRed,
                    Fill = System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "Profit",
                    Values = ProfitValues,
                    Stroke = System.Windows.Media.Brushes.LawnGreen,
                    Fill = System.Windows.Media.Brushes.Transparent
                }
            };
        }


    }

    public class Transaction
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
    }
}
