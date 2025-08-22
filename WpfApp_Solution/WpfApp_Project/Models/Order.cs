using System;
using System.ComponentModel.DataAnnotations;
using WpfApp_Project.Models.Enum;

namespace WpfApp_Project.Models
{
    public class Order
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "A Pessoa é obrigatória")]
        public Person Person { get; set; }

        [Required(ErrorMessage = "É necessário adicionar um produto")]
        public Product[] Products { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DateOfSale { get; set; }

        [Required(ErrorMessage = "A Forma de pagamento é obrigatória")]
        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus Status {  get; set; }
    }
}
