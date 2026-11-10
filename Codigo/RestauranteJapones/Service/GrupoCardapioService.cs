using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class GrupoCardapioService : IGrupoCardapioService
    {
        private readonly RestauranteContext context;

        public GrupoCardapioService(RestauranteContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Criar um novo grupo de cardápio
        /// </summary>
        /// <param name="grupoCardapio">Dados do grupo de cardápio</param>
        /// <returns>Id do grupo de cardápio criado</returns>
        public uint Create(Grupocardapio grupoCardapio)
        {
            context.Add(grupoCardapio);
            context.SaveChanges();
            return grupoCardapio.Id;
        }

        /// <summary>
        /// Remover um grupo de cardápio da base de dados
        /// </summary>
        /// <param name="id">Id do grupo de cardápio</param>
        public void Delete(uint id)
        {
            var grupoCardapio = context.Grupocardapios.Find(id);
            if (grupoCardapio != null)
            {
                context.Remove(grupoCardapio);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Editar um grupo de cardápio na base de dados
        /// </summary>
        /// <param name="grupoCardapio">Dados do grupo de cardápio</param>
        public void Edit(Grupocardapio grupoCardapio)
        {
            context.Update(grupoCardapio);
            context.SaveChanges();
        }

        /// <summary>
        /// Buscar um grupo de cardápio na base de dados
        /// </summary>
        /// <param name="id">Id do grupo de cardápio</param>
        /// <returns>Retorna o grupo de cardápio encontrado</returns>
        public Grupocardapio Get(uint id)
        {
            return context.Grupocardapios.FirstOrDefault(g => g.Id == id)!;
        }

        /// <summary>
        /// Buscar todos os grupos de cardápio na base de dados
        /// </summary>
        /// <returns>Retorna uma lista de todos os grupos de cardápio</returns>
        public IEnumerable<Grupocardapio> GetAll()
        {
            return context.Grupocardapios.AsNoTracking();
        }
    }
}
