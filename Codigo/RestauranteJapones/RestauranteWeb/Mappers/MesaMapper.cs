using Core;
using RestauranteWeb.Models;

namespace RestauranteWeb.Mappers
{
    public static class MesaMapper
    {
        public static Mesa ToEntity(MesaViewModel viewModel)
        {
            return new Mesa
            {
                Id = viewModel.Id,
                Identificacao = viewModel.Identificacao.Trim(),
                IdRestaurante = viewModel.IdRestaurante
            };
        }

        public static MesaViewModel ToViewModel(Mesa entity)
        {
            return new MesaViewModel
            {
                Id = entity.Id,
                Identificacao = entity.Identificacao,
                IdRestaurante = entity.IdRestaurante
            };
        }

        public static IEnumerable<MesaViewModel> ToViewModel(IEnumerable<Mesa> entities)
        {
            return entities.Select(ToViewModel);
        }

        public static void UpdateEntity(Mesa entity, MesaViewModel viewModel)
        {
            entity.Identificacao = viewModel.Identificacao.Trim();
            entity.IdRestaurante = viewModel.IdRestaurante;
        }
    }
}