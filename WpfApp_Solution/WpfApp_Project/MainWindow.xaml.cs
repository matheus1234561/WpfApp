using System.Windows;
using WpfApp_Project.Views.Person_Pages;

namespace WpfApp_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PersonWindow personWindow = new PersonWindow();
            personWindow.ShowDialog();
        }
    }
}
