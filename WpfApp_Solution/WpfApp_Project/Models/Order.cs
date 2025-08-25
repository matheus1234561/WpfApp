using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WpfApp_Project.Models.Enum;

namespace WpfApp_Project.Models
{
    public class Order : INotifyPropertyChanged
    {
        private Person _person;
        private List<Product> _products;
        private PaymentMethod _paymentMethod;
        private PaymentStatus _status;

        public int Id { get; set; }

        [Required(ErrorMessage = "A Pessoa é obrigatória")]
        public Person Person
        {
            get { return _person; }
            set
            {
                if (_person != value)
                {
                    _person = value;
                    OnPropertyChanged(nameof(Person));
                }
            }
        }

        [Required(ErrorMessage = "É necessário adicionar um produto")]
        public List<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }

        public decimal TotalPrice { get; set; }

        public string DateOfSale { get; set; }

        [Required(ErrorMessage = "A Forma de pagamento é obrigatória")]
        public PaymentMethod PaymentMethod
        {
            get { return _paymentMethod; }
            set
            {
                if (_paymentMethod != value)
                {
                    _paymentMethod = value;
                    OnPropertyChanged(nameof(PaymentMethod));
                }
            }
        }

        public PaymentStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
