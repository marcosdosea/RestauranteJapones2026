using System.ComponentModel.DataAnnotations;

namespace RestauranteWeb.Models
{
    public class FuncionarioViewModel
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        public uint Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 45 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo CPF deve ter entre 11 e 14 caracteres.")]
        public string Cpf { get; set; }

        [StringLength(9, MinimumLength = 8, ErrorMessage = "O campo CEP deve ter entre 8 e 9 caracteres.")]
        public string? Cep { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo Rua deve ter entre 3 e 50 caracteres.")]
        public string? Rua { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo Bairro deve ter entre 3 e 50 caracteres.")]
        public string? Bairro { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "O campo Cidade deve ter entre 3 e 30 caracteres.")]
        public string? Cidade { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "O campo Estado deve ter exatamente 2 caracteres.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O campo Telefone1 deve ter entre 10 e 15 caracteres.")]
        public string? Telefone1 { get; set; }

        [StringLength(15, MinimumLength = 10, ErrorMessage = "O campo Telefone2 deve ter entre 10 e 15 caracteres.")]
        public string? Telefone2 { get; set; }

        [Display(Name = "Restaurante")]
        public uint IdRestaurante { get; set; }

        [Display(Name = "Tipo de funcionário")]
        public uint IdTipoFuncionario { get; set; }

        [Display(Name = "Restaurante")]
        [Required(ErrorMessage = "O restaurante é obrigatório.")]
        public string? NomeRestaurante { get; set; }

        [Display(Name = "Tipo de Funcionário")]
        [Required(ErrorMessage = "O tipo de funcionário é obrigatório.")]
        public string? NomeTipoFuncionario { get; set; }
    }
}
