using System.ComponentModel.DataAnnotations;

namespace WpfApp_Project.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CPF { get; set; }

        public string Address { get; set; }
    }
}
