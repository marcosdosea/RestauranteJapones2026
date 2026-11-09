using Core;
using Mapster;
using RestauranteWeb.Models;

namespace RestauranteWeb.Mappers
{
    public class TipoRestauranteMapper
    {
        public static TipoRestauranteViewModel ToViewModel(Tiporestaurante tipoRestaurante)
        {
            return tipoRestaurante.Adapt<TipoRestauranteViewModel>();
        }

        public static Tiporestaurante ToEntity(TipoRestauranteViewModel tipoRestauranteViewModel)
        {
            return tipoRestauranteViewModel.Adapt<Tiporestaurante>();
        }

        public static IEnumerable<TipoRestauranteViewModel> ToViewModel(IEnumerable<Tiporestaurante>? tiposRestaurante)
        {
            return tiposRestaurante?.Adapt<IEnumerable<TipoRestauranteViewModel>>() ?? new List<TipoRestauranteViewModel>();
        }

        public static IEnumerable<Tiporestaurante> ToEntity(IEnumerable<TipoRestauranteViewModel>? tiposRestauranteViewModel)
        {
            return tiposRestauranteViewModel?.Adapt<IEnumerable<Tiporestaurante>>() ?? new List<Tiporestaurante>();
        }
    }
}
