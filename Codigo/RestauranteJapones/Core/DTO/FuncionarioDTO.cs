using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class FuncionarioDTO
    {
        public uint Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Telefone1 { get; set; }
        public string NomeRestaurante { get; set; } = null!;
        public string NomeTipoFuncionario { get; set; } = null!;
    }
}
