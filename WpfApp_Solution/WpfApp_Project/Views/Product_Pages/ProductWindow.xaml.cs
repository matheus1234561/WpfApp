using System.Windows;
using WpfApp_Project.ViewModels;

namespace WpfApp_Project.Views.Product_Pages
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            this.DataContext = new ProductViewModel();
        }
    }
}
