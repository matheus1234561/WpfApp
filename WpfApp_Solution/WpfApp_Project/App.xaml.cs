using System.Windows;
using WpfApp_Project.Views.Person_Pages;
using WpfApp_Project.Views.Product_Pages;

namespace WpfApp_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            
            var personsWindow = new PersonWindow();
            var productWindow = new ProductWindow();
            //personsWindow.Show();
            productWindow.Show();
        }
    }
}
