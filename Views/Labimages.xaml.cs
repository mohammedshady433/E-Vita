using E_Vita.Interfaces.Repository;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Microsoft.Extensions.DependencyInjection;
using E_Vita.Models;

namespace E_Vita.Views
{
    public partial class Labimages : Page
    {
        private readonly IRepository<Models.LabTest> _LabTestRepo;

        // For Future Use
        public Labimages(int patientId)
        {
            InitializeComponent();
            LoadLabTests(patientId);
            var services = ((App)Application.Current)._serviceProvider;
            _LabTestRepo = services.GetService<IRepository<Models.LabTest>>() ?? throw new InvalidOperationException("Data helper service is not available");

        }
        public ObservableCollection<LabTest> labsofthepatient { get; set; }
        public Labimages()
        {
            InitializeComponent();
            var services = ((App)Application.Current)._serviceProvider;
            _LabTestRepo = services.GetService<IRepository<Models.LabTest>>() ?? throw new InvalidOperationException("Data helper service is not available");
            labsofthepatient = new ObservableCollection<LabTest>();
        }

        private async void LoadLabTests(int patientId)
        {

            var allimages = await _LabTestRepo.GetAllAsync();

            var filteredlabTests = allimages
                    .Where(lt => lt.PatientId == patientId)
                    .ToList();

            labsofthepatient.Clear();
            foreach (var labTest in filteredlabTests)
            {
                labsofthepatient.Add(labTest);
            }
            LabTestImagesControl.ItemsSource = labsofthepatient;



        }

        private System.Windows.Media.ImageSource LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            using (var stream = new MemoryStream(imageData))
            {
                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }
        }

        private void Images(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(searchtxt.Text);
            LoadLabTests(id);
        }
    }

    public class LabTestViewModel
    {
        public System.Windows.Media.ImageSource ImageSource { get; set; }
    }
}