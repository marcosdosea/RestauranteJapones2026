using System.ComponentModel.DataAnnotations;

namespace Util
{
    public sealed class CnpjAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
                return true;

            var cnpj = Methods.RemoveNaoNumericos(value.ToString()!);
            return cnpj.Length == 14
                && cnpj.Distinct().Count() > 1
                && Methods.ValidarCnpj(cnpj);
        }
    }
}
