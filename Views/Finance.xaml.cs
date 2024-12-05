using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace E_Vita
{
    public partial class FinancePage : Page
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public ObservableCollection<ChartValues<double>> RevenueValues { get; set; }
        public ObservableCollection<ChartValues<double>> ExpenseValues { get; set; }
        public ObservableCollection<PieChartEntry> DepartmentRevenue { get; set; }

        public FinancePage()
        {
            InitializeComponent();


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

            // Check if the DataGrid ItemsSource is being set correctly
            Debug.WriteLine($"DataGrid ItemSource Count: {((ObservableCollection<Transaction>)TransactionsDataGrid.ItemsSource).Count}"); // Debug log

            // Set up data for the charts
            RevenueValues = new ObservableCollection<ChartValues<double>>()
            {
                new ChartValues<double> { 50000, 60000, 70000, 80000, 75000 }
            };

            ExpenseValues = new ObservableCollection<ChartValues<double>>()
            {
                new ChartValues<double> { 20000, 25000, 30000, 35000, 40000 }
            };

            var departmentValues = new ChartValues<double> { 30, 40, 30 };

            // Bind data to charts
            RevenueExpensesChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Revenue",
                    Values = RevenueValues[0]
                },
                new LineSeries
                {
                    Title = "Expenses",
                    Values = ExpenseValues[0]
                }
            };
            ////------------------------------
            //RevenueByDepartmentChart.Series = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "HR",
            //        Values = new ChartValues<double> { departmentValues[0] },
            //        DataLabels = true,
            //        LabelPoint = chartPoint => chartPoint.Y + "%"
            //    },
            //    new PieSeries
            //    {
            //        Title = "Sales",
            //        Values = new ChartValues<double> { departmentValues[1] },
            //        DataLabels = true,
            //        LabelPoint = chartPoint => chartPoint.Y + "%"
            //    },
            //    new PieSeries
            //    {
            //        Title = "R&D",
            //        Values = new ChartValues<double> { departmentValues[2] },
            //        DataLabels = true,
            //        LabelPoint = chartPoint => chartPoint.Y + "%"
            //    }
            //};
//------------------------------------------------------
            // Explicitly set the DataContext
            this.DataContext = this;
        }
    }

    public class PieChartEntry
    {
        public double Value { get; set; }
        public string Label { get; set; }
        public string ValueLabel { get; set; }
        public System.Windows.Media.Color Color { get; set; }

        public PieChartEntry(double value)
        {
            Value = value;
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
