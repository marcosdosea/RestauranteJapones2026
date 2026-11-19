using System.ComponentModel.DataAnnotations;
using Util;

namespace RestauranteWeb.Models
{
    public class RestauranteViewModel
    {
        [Display(Name = "Código")]
        public uint Id { get; set; }

        [Display(Name = "Tipo de restaurante")]
        [Required(ErrorMessage = "O tipo de restaurante é obrigatório")]
        public uint IdTipoRestaurante { get; set; }

        [Display(Name = "Nome do tipo de restaurante")]
        public string? NomeTipoRestaurante { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "O CNPJ é obrigatório")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter entre 14 e 18 caracteres")]
        [Cnpj(ErrorMessage = "CNPJ inválido")]
        public string Cnpj { get; set; } = null!;

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 45 caracteres")]
        public string Nome { get; set; } = null!;

        [Display(Name = "CEP")]
        [StringLength(9, ErrorMessage = "O CEP deve ter no máximo 9 caracteres")]
        [Cep(ErrorMessage = "CEP inválido")]
        public string? Cep { get; set; }

        [Display(Name = "Rua")]
        [StringLength(45, ErrorMessage = "A rua deve ter no máximo 45 caracteres")]
        public string? Rua { get; set; }

        [Display(Name = "Bairro")]
        [StringLength(45, ErrorMessage = "O bairro deve ter no máximo 45 caracteres")]
        public string? Bairro { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(45, ErrorMessage = "A cidade deve ter no máximo 45 caracteres")]
        public string? Cidade { get; set; }

        [Display(Name = "Estado")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ter 2 caracteres")]
        public string? Estado { get; set; }

        [Display(Name = "Telefone 1")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres")]
        [Telefone(ErrorMessage = "Telefone 1 inválido")]
        public string? Telefone1 { get; set; }

        [Display(Name = "Telefone 2")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres")]
        [Telefone(ErrorMessage = "Telefone 2 inválido")]
        public string? Telefone2 { get; set; }
    }
}
