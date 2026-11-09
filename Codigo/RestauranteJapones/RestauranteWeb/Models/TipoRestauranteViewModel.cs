using System.ComponentModel.DataAnnotations;

namespace RestauranteWeb.Models
{
    public class TipoRestauranteViewModel
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O código do tipo de restaurante é obrigatório")]
        public uint Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "O nome do tipo de restaurante deve ter entre 3 e 45 caracteres")]
        public string Nome { get; set; }
    }
}
