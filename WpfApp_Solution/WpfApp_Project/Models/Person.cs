using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfApp_Project.Models
{
    public class Person : INotifyPropertyChanged
    {
        private string _name;
        private string _cpf;
        private string _address;
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

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CPF
        {
            get { return _cpf; }
            set
            {
                if (_cpf != value)
                {
                    _cpf = value;
                    OnPropertyChanged(nameof(CPF));
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
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
