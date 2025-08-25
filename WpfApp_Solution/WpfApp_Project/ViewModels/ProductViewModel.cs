using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp_Project.command;
using WpfApp_Project.Models;
using WpfApp_Project.Services;
using WpfApp_Project.ViewModels.baseModel;

namespace WpfApp_Project.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private ProductService _productService;
        private Product _product;

        private string _filterName;
        private decimal _filterPrice;

        public ProductViewModel()
        {
            _productService = new ProductService();
            Product = new Product();
            Products = new ObservableCollection<Product>(_productService.LoadProductFromXml());
            FiltredProducts = new ObservableCollection<Product>(Products);


            SaveCommand = new RelayCommand(SaveProducts);
            EditCommand = new RelayCommand(EditProduct);
            ExcludeCommand = new RelayCommand(ExcludeProduct);
        }

        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ExcludeCommand { get; }

        private ObservableCollection<Product> _products { get; set; }

        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private ObservableCollection<Product> _filtredPProducts { get; set; }


        public ObservableCollection<Product> FiltredProducts
        {
            get
            {
                return _filtredPProducts;
            }
            set
            {
                _filtredPProducts = value;
                OnPropertyChanged(nameof(FiltredProducts));
            }
        }

        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private void SaveProducts(object product)
        {

            if (string.IsNullOrEmpty(FiltredProducts.Last().Name) || FiltredProducts.Last().Price <= 0)
            {
                MessageBox.Show("Produto inválido. Verifique os dados.");
                return;
            }

            FiltredProducts.Last().Id = _productService.GenerateLastId();

            _productService.SaveProduct(new List<Product>(FiltredProducts));

            ApplyFilter();

            Product = new Product();
        }

        private void EditProduct(object product)
        {
            if (product is Product p)
            {
                _productService.ProductEdit(p);
                var index = FiltredProducts.IndexOf(p);
                if (index >= 0)
                {
                    FiltredProducts[index] = p;
                }
            }
        }

        private void ExcludeProduct(object product)
        {
            if (product is Product p)
            {
                _productService.ProductExclude(p);
                FiltredProducts.Remove(p);
            }
        }


        private void ApplyFilter()
        {
            var filterName = FilterName ?? "";

            var listProduct = _productService.LoadProductFromXml();

            var filteredList = listProduct.Where(p => (p.Name != null && p.Name.ToLower().Contains(filterName.ToLower()))).ToList();

            FiltredProducts.Clear();
            foreach (var product in filteredList)
            {
                FiltredProducts.Add(product);
            }

            OnPropertyChanged(nameof(FiltredProducts));
        }

    }
}
