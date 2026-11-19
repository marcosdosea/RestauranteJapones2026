using System.ComponentModel.DataAnnotations;

namespace RestauranteWeb.Models
{
    public class ItemCardapioViewModel
    {
        [Display(Name = "Código")]
        public uint Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome do item é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do item deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Display(Name = "Preço")]
        [Range(typeof(decimal), "0", "99999999.99", ErrorMessage = "O preço deve estar entre 0 e 99999999,99")]
        public decimal? Preco { get; set; }

        [Display(Name = "Detalhes")]
        [StringLength(500, ErrorMessage = "Os detalhes devem ter no máximo 500 caracteres")]
        public string? Detalhes { get; set; }

        [Display(Name = "Ativo")]
        [Range(0, 1, ErrorMessage = "Informe se o item está ativo")]
        public sbyte Ativo { get; set; } = 1;

        [Display(Name = "Disponível")]
        [Required(ErrorMessage = "Informe se o item está disponível")]
        [StringLength(45, ErrorMessage = "A disponibilidade deve ter no máximo 45 caracteres")]
        public string Disponivel { get; set; } = "1";

        [Display(Name = "Restaurante")]
        [Required(ErrorMessage = "O restaurante é obrigatório")]
        public uint IdRestaurante { get; set; }

        [Display(Name = "Grupo do cardápio")]
        [Required(ErrorMessage = "O grupo do cardápio é obrigatório")]
        public uint IdGrupoCardapio { get; set; }

        [Display(Name = "Restaurante")]
        public string? NomeRestaurante { get; set; }

        [Display(Name = "Grupo do cardápio")]
        public string? NomeGrupoCardapio { get; set; }

        public string AtivoTexto => Ativo == 1 ? "Sim" : "Não";

        public string DisponivelTexto => Disponivel == "1" ? "Sim" : "Não";
    }
}
