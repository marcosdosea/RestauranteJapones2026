using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class TipoRestauranteService : ITipoRestauranteService
    {
        private readonly RestauranteContext context;

        public TipoRestauranteService(RestauranteContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Criar um novo tipo de restaurante
        /// </summary>
        /// <param name="tipoRestaurante">Dados do tipo de restaurante</param>
        /// <returns>id do tipo de restaurante criado</returns>
        public uint Create(Tiporestaurante tipoRestaurante)
        {
            context.Add(tipoRestaurante);
            context.SaveChanges();
            return tipoRestaurante.Id;
        }

        /// <summary>
        /// Remover um tipo de restaurante da base de dados
        /// </summary>
        /// <param name="id">Id do tipo de restaurante</param>
        public void Delete(uint id)
        {
            var tipoRestaurante = context.Tiporestaurantes.Find(id);
            if (tipoRestaurante != null)
            {
                context.Remove(tipoRestaurante);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Editar um tipo de restaurante na base de dados
        /// </summary>
        /// <param name="tipoRestaurante">Dados do tipo de restaurante</param>
        public void Edit(Tiporestaurante tipoRestaurante)
        {
            context.Update(tipoRestaurante);
            context.SaveChanges();
        }

        /// <summary>
        /// Buscar um tipo de restaurante na base de dados
        /// </summary>
        /// <param name="id">Id do tipo de restaurante</param>
        /// <returns>Retorna o tipo de restaurante encontrado ou null</returns>
        public Tiporestaurante Get(uint id)
        {
            return context.Tiporestaurantes.FirstOrDefault(t => t.Id == id)!;
        }

        /// <summary>
        /// Buscar todos os tipos de restaurante na base de dados
        /// </summary>
        /// <returns>Retorna uma lista de todos os tipos de restaurante</returns>
        public IEnumerable<Tiporestaurante> GetAll()
        {
            return context.Tiporestaurantes.AsNoTracking();
        }
    }
}
