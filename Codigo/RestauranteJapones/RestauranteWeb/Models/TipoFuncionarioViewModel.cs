using System.ComponentModel.DataAnnotations;

namespace RestauranteWeb.Models
{
    public class TipoFuncionarioViewModel
    {
        [Display(Name = "Código")]
        public uint Id { get; set; }

        [Required(ErrorMessage = "O nome do tipo de funcionário é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;
    }
}
