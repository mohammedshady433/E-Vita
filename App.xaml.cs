using System.Configuration;
using System.Data;
using System.Windows;
using Syncfusion;
using Syncfusion.Licensing;
namespace E_Vita
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Register the Syncfusion license key
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cWWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFiWH1WcnVVQmNYUk1wWw==");
        }
    }

}