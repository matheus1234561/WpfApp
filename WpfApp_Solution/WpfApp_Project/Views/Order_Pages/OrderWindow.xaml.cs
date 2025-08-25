using System;
using System.Windows;
using WpfApp_Project.Models;
using WpfApp_Project.ViewModels;

namespace WpfApp_Project.Views.Order_Pages
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow(Person selectedPerson)
        {
            InitializeComponent();
            this.DataContext = new OrderViewModel(selectedPerson);
        }
    }
}
