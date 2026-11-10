using Core;
using Mapster;
using RestauranteWeb.Models;
using System.Collections.Generic;

namespace RestauranteWeb.Mappers
{
    public static class GrupoCardapioMapper
    {
        public static GrupocardapioViewModel ToViewModel(Grupocardapio grupoCardapio)
        {
            return grupoCardapio.Adapt<GrupocardapioViewModel>();
        }

        public static Grupocardapio ToEntity(GrupocardapioViewModel grupoCardapioViewModel)
        {
            return grupoCardapioViewModel.Adapt<Grupocardapio>();
        }

        public static IEnumerable<GrupocardapioViewModel> ToViewModel(IEnumerable<Grupocardapio>? gruposCardapio)
        {
            return gruposCardapio?.Adapt<IEnumerable<GrupocardapioViewModel>>() ?? new List<GrupocardapioViewModel>();
        }

        public static IEnumerable<Grupocardapio> ToEntity(IEnumerable<GrupocardapioViewModel>? gruposCardapioViewModel)
        {
            return gruposCardapioViewModel?.Adapt<IEnumerable<Grupocardapio>>() ?? new List<Grupocardapio>();
        }
    }
}