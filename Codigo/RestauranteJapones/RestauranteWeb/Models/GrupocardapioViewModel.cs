using System.ComponentModel.DataAnnotations;

namespace RestauranteWeb.Models
{
    public class GrupocardapioViewModel
    {
        [Key]
        public uint Id { get; set; }

        [Required(ErrorMessage = "O nome do grupo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres")]
        [Display(Name = "Nome do Grupo")]
        public string Nome { get; set; } = string.Empty;
    }
}
