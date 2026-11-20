using Core;
using Core.DTO;
using Mapster;
using RestauranteWeb.Models;

namespace RestauranteWeb.Mappers
{
    public static class FuncionarioMapper
    {
        public static FuncionarioViewModel ToViewModel(Funcionario funcionario)
        {
            return funcionario.Adapt<FuncionarioViewModel>();
        }

        public static Funcionario ToEntity(FuncionarioViewModel funcionarioViewModel)
        {
            return funcionarioViewModel.Adapt<Funcionario>();
        }
        public static IEnumerable<FuncionarioViewModel> ToViewModel(IEnumerable<FuncionarioDTO>? lista)
        {
            return lista?.Adapt<IEnumerable<FuncionarioViewModel>>() ?? new List<FuncionarioViewModel>();
        }
    }
}
