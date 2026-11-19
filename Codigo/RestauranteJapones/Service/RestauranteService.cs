using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class RestauranteService : IRestauranteService
    {
        private readonly RestauranteContext context;

        public RestauranteService(RestauranteContext context)
        {
            this.context = context;
        }

        public uint Create(Restaurante restaurante)
        {
            context.Restaurantes.Add(restaurante);
            context.SaveChanges();
            return restaurante.Id;
        }

        public void Edit(Restaurante restaurante)
        {
            context.Restaurantes.Update(restaurante);
            context.SaveChanges();
        }

        public void Delete(uint id)
        {
            var restaurante = context.Restaurantes.Find(id);
            if (restaurante is not null)
            {
                context.Restaurantes.Remove(restaurante);
                context.SaveChanges();
            }
        }

        public Restaurante Get(uint id)
        {
            return context.Restaurantes
                .Include(r => r.IdTipoRestauranteNavigation)
                .FirstOrDefault(r => r.Id == id)!;
        }

        public IEnumerable<Restaurante> GetAll()
        {
            return context.Restaurantes
                .Include(r => r.IdTipoRestauranteNavigation)
                .AsNoTracking();
        }
    }
}
