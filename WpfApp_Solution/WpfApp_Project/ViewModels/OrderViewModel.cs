using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using WpfApp_Project.command;
using WpfApp_Project.command.Generic;
using WpfApp_Project.Models;
using WpfApp_Project.Models.Enum;
using WpfApp_Project.Services;
using WpfApp_Project.ViewModels.baseModel;

namespace WpfApp_Project.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        private OrderService _orderService;
        private ProductService _productService;
        private Order _order;

        private ObservableCollection<Order> _orders { get; set; }

        public ObservableCollection<Order> Orders
        {
            get
            {
                return _orders;
            }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private ObservableCollection<Order> _filtredOrders { get; set; }


        public ObservableCollection<Order> FiltredOrders
        {
            get
            {
                return _filtredOrders;
            }
            set
            {
                _filtredOrders = value;
                OnPropertyChanged(nameof(FiltredOrders));
            }
        }

        public ObservableCollection<Product> AvailableProducts { get; set; }

        public OrderViewModel(Person selectedPerson)
        {
            _orderService = new OrderService();
            _productService = new ProductService();

            OrderProducts = new ObservableCollection<Product>();

            AvailableProducts = new ObservableCollection<Product>(_productService.LoadProductFromXml());

            Person = selectedPerson;

            Order = new Order { Person = Person, Products = new List<Product>() };

            Orders = new ObservableCollection<Order>(_orderService.LoadOrderFromXml());
            FiltredOrders = new ObservableCollection<Order>(Orders);
           


            SaveCommand = new RelayCommand(SaveOrders);
            AddProductToOrderCommand = new RelayCommand<Product>(AddProductToOrder);
            RemoveProductFromOrderCommand = new RelayCommand<Product>(RemoveProductFromOrder);

        }

        public Order Order { get; set; }
        public Person Person { get; set; }

        public ObservableCollection<Product> OrderProducts { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand AddProductToOrderCommand { get; set; }
        public ICommand RemoveProductFromOrderCommand { get; set; }

        private void SaveOrders(object parameter)
        {

            Order.Id = _orderService.GenerateLastId();
            Order.Person = new Person 
            { 
                Id = Orders.Last().Person.Id, 
                Name = Orders.Last().Person.Name, 
                CPF = Orders.Last().Person.CPF,
                Address = Orders.Last().Person.Address,
            };
            Order.Products = Orders.Last().Products;
            Order.TotalPrice = Orders.Last().TotalPrice;
            Order.DateOfSale = DateTime.Now;
            Order.PaymentMethod = PaymentMethod.Card;
            Order.Status = PaymentStatus.pending;

            List<Order> finalOrder = new List<Order>();
            finalOrder.Add(Order);

            _orderService.SaveOrder(finalOrder);

            Order = new Order();
        }

        private void EditOrder(object order)
        {
            if (order is Order o)
            {
                _orderService.OrderEdit(o);
                var index = Orders.IndexOf(o);
                if (index >= 0)
                {
                    Orders[index] = o;
                }

                ApplyFilter();
            }
        }

        private void ExcludeOrder(object order)
        {
            if (order is Order o)
            {
                _orderService.OrderExclude(o);
                Orders.Remove(o);

                ApplyFilter();
            }
        }

        private void AddProductToOrder(Product product)
        {
            Order.Products.Add(product);
            Order.TotalPrice += product.Price;
            Orders.Add(Order);
        }

        private void RemoveProductFromOrder(Product product)
        {
            Order.Products.Add(product);
            Order.TotalPrice -= product.Price;

            if (Orders.Contains(Order))
            { 
                Orders.Remove(Order); 
            }
        }


        private void ApplyFilter()
        {
            //var filterName = FilterName ?? "";
            //var filterCPF = FilterCPF ?? "";

            //var filteredList = Persons.Where(p => (p.Name != null && p.Name.ToLower().Contains(filterName.ToLower())) &&
            //                                       (p.CPF != null && p.CPF.ToLower().Contains(filterCPF.ToLower()))).ToList();

            //FiltredPersons.Clear();
            //foreach (var person in filteredList)
            //{
            //    FiltredPersons.Add(person);
            //}

            //OnPropertyChanged(nameof(FiltredPersons));
        }
    }
}
