using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class TipoFuncionarioService : ITipoFuncionario
    {
        private readonly RestauranteContext context;

        public TipoFuncionarioService(RestauranteContext context)
        {
            this.context = context;
        }

        public uint Create(Tipofuncionario tipoFuncionario)
        {
            context.Add(tipoFuncionario);
            context.SaveChanges();
            return tipoFuncionario.Id;
        }

        public void Edit(Tipofuncionario tipoFuncionario)
        {
            context.Update(tipoFuncionario);
            context.SaveChanges();
        }

        public void Delete(uint id)
        {
            var tipoFuncionario = context.Tipofuncionarios.Find(id);
            if (tipoFuncionario != null)
            {
                context.Remove(tipoFuncionario);
                context.SaveChanges();
            }
        }

        public Tipofuncionario Get(uint id)
        {
            return context.Tipofuncionarios.FirstOrDefault(t => t.Id == id)!;
        }

        public IEnumerable<Tipofuncionario> GetAll()
        {
            return context.Tipofuncionarios.AsNoTracking();
        }
    }
}
