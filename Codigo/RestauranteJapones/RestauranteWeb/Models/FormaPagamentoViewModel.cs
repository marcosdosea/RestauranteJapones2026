using System.ComponentModel.DataAnnotations;

namespace RestauranteWeb.Models
{
    public class FormaPagamentoViewModel
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O código da forma de pagamento é obrigatório")]
        public uint Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome da forma de pagamento deve ter entre 3 e 50 caracteres")]
        public string Nome { get; set; } = null!;
    }
}
