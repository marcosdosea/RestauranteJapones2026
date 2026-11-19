using Core;
using Mapster;
using RestauranteWeb.Models;

namespace RestauranteWeb.Mappers
{
    public static class ItemCardapioMapper
    {
        public static ItemCardapioViewModel ToViewModel(Itemcardapio itemCardapio)
        {
            var viewModel = itemCardapio.Adapt<ItemCardapioViewModel>();
            viewModel.NomeRestaurante = itemCardapio.IdRestauranteNavigation?.Nome;
            viewModel.NomeGrupoCardapio = itemCardapio.IdGrupoCardapioNavigation?.Nome;
            return viewModel;
        }

        public static Itemcardapio ToEntity(ItemCardapioViewModel itemCardapioViewModel)
        {
            return itemCardapioViewModel.Adapt<Itemcardapio>();
        }

        public static IEnumerable<ItemCardapioViewModel> ToViewModel(IEnumerable<Itemcardapio>? itensCardapio)
        {
            return itensCardapio?.Select(ToViewModel) ?? Enumerable.Empty<ItemCardapioViewModel>();
        }
    }
}
