using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class ItemCardapioService : IItemCardapioService
    {
        private readonly RestauranteContext context;

        public ItemCardapioService(RestauranteContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Criar um novo item do cardápio
        /// </summary>
        /// <param name="itemCardapio">Dados do item do cardápio</param>
        /// <returns>Id do item do cardápio criado</returns>
        public uint Create(Itemcardapio itemCardapio)
        {
            context.Itemcardapios.Add(itemCardapio);
            context.SaveChanges();
            return itemCardapio.Id;
        }

        /// <summary>
        /// Remover um item do cardápio da base de dados
        /// </summary>
        /// <param name="id">Id do item do cardápio</param>
        public void Delete(int id)
        {
            var itemCardapio = context.Itemcardapios.Find((uint)id);
            if (itemCardapio is not null)
            {
                context.Itemcardapios.Remove(itemCardapio);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Editar um item do cardápio na base de dados
        /// </summary>
        /// <param name="itemCardapio">Dados do item do cardápio</param>
        public void Edit(Itemcardapio itemCardapio)
        {
            context.Itemcardapios.Update(itemCardapio);
            context.SaveChanges();
        }

        /// <summary>
        /// Buscar um item do cardápio na base de dados
        /// </summary>
        /// <param name="id">Id do item do cardápio</param>
        /// <returns>Retorna o item do cardápio encontrado</returns>
        public Itemcardapio Get(int id)
        {
            return context.Itemcardapios
                .Include(item => item.IdRestauranteNavigation)
                .Include(item => item.IdGrupoCardapioNavigation)
                .FirstOrDefault(item => item.Id == (uint)id)!;
        }

        /// <summary>
        /// Buscar todos os itens do cardápio na base de dados
        /// </summary>
        /// <returns>Retorna uma lista de todos os itens do cardápio</returns>
        public IEnumerable<Itemcardapio> GetAll()
        {
            return context.Itemcardapios
                .Include(item => item.IdRestauranteNavigation)
                .Include(item => item.IdGrupoCardapioNavigation)
                .AsNoTracking();
        }
    }
}
