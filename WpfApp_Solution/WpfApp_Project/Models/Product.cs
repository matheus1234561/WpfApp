using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfApp_Project.Models
{
    public class Product : INotifyPropertyChanged
    {
        private string _name;
        private string _code;
        private decimal _price;
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        [Required(ErrorMessage = "O Código é obrigatório")]
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged(nameof(Code));
                }
            }
        }


        [Required(ErrorMessage = "O Preço é obrigatório")]
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
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
