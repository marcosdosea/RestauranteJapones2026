using Core;
using Mapster;
using RestauranteWeb.Models;

namespace RestauranteWeb.Mappers
{
    public static class RestauranteMapper
    {
        public static RestauranteViewModel ToViewModel(Restaurante restaurante)
        {
            var viewModel = restaurante.Adapt<RestauranteViewModel>();
            viewModel.NomeTipoRestaurante = restaurante.IdTipoRestauranteNavigation?.Nome;
            return viewModel;
        }

        public static Restaurante ToEntity(RestauranteViewModel restauranteViewModel)
        {
            var restaurante = restauranteViewModel.Adapt<Restaurante>();
            restaurante.Cnpj = Util.Methods.RemoveNaoNumericos(restaurante.Cnpj);
            return restaurante;
        }

        public static IEnumerable<RestauranteViewModel> ToViewModel(IEnumerable<Restaurante>? restaurantes)
        {
            return restaurantes?.Select(ToViewModel) ?? Enumerable.Empty<RestauranteViewModel>();
        }
    }
}
