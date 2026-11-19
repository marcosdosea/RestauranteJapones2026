using System.ComponentModel.DataAnnotations;

namespace Util
{
    public sealed class TelefoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
                return true;

            var telefone = Methods.RemoveNaoNumericos(value.ToString()!);
            return (telefone.Length == 10 || telefone.Length == 11)
                && !telefone.StartsWith('0')
                && telefone.Distinct().Count() > 1;
        }
    }
}
