using Core;
using Mapster;
using RestauranteWeb.Models;

namespace RestauranteWeb.Mappers
{
    public class TipoFuncionarioMapper
    {
        public static TipoFuncionarioViewModel ToViewModel(Tipofuncionario tipoFuncionario)
        {
            return tipoFuncionario.Adapt<TipoFuncionarioViewModel>();
        }

        public static Tipofuncionario ToEntity(TipoFuncionarioViewModel tipoFuncionarioViewModel)
        {
            return tipoFuncionarioViewModel.Adapt<Tipofuncionario>();
        }

        public static IEnumerable<TipoFuncionarioViewModel> ToViewModel(IEnumerable<Tipofuncionario>? tiposFuncionario)
        {
            return tiposFuncionario?.Adapt<IEnumerable<TipoFuncionarioViewModel>>() ?? new List<TipoFuncionarioViewModel>();
        }
    }
}
