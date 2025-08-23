using System.Windows;
using WpfApp_Project.ViewModels;

namespace WpfApp_Project.Views.Person_Pages
{
    /// <summary>
    /// Interaction logic for PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        public PersonWindow()
        {
            InitializeComponent();

            this.DataContext = new PersonViewModel();
        }
    }
}
