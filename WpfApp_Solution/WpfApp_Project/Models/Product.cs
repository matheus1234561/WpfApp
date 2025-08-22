using System.ComponentModel.DataAnnotations;

namespace WpfApp_Project.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Código é obrigatório")]
        public string Code { get; set; }

        public decimal Price { get; set; }
    }
}
