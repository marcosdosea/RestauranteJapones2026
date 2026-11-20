using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestauranteWeb.Models
{
    public class MesaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
        [DisplayName("Identificação da Mesa")]
        public string Identificacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [Range(1, uint.MaxValue, ErrorMessage = "Selecione um {0} válido.")]
        [DisplayName("Restaurante")]
        public uint IdRestaurante { get; set; }

        public SelectList? ListaRestaurantes { get; set; }
    }
}