using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly RestauranteContext context;

        public FuncionarioService(RestauranteContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Cria um novo funcionário no banco de dados.
        /// </summary>
        /// <param name="funcionario">O funcionário a ser criado.</param>
        /// <returns>O ID do funcionário criado.</returns>
        public uint Create(Funcionario funcionario)
        {
            context.Funcionarios.Add(funcionario);
            context.SaveChanges();
            return funcionario.Id;
        }

        /// <summary>
        /// Edita um funcionário existente no banco de dados.
        /// </summary>
        /// <param name="funcionario">O funcionário a ser editado.</param>
        public void Edit(Funcionario funcionario)
        {
            if (funcionario != null)
            {
                context.Funcionarios.Update(funcionario);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Remove um funcionário do banco de dados pelo ID.
        /// </summary>
        /// <param name="id">O ID do funcionário a ser removido.</param>
        public void Delete(uint id)
        {
            var funcionario = context.Funcionarios.Find(id);
            if (funcionario != null)
            {
                context.Funcionarios.Remove(funcionario);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Busca um funcionário pelo ID.
        /// </summary>
        /// <param name="id">O ID do funcionário a ser buscado.</param>
        /// <returns>O funcionário encontrado ou null se não encontrado.</returns>
        public Funcionario? Get(uint id)
        {
            return context.Funcionarios
                .Include(f => f.IdRestauranteNavigation)
                .Include(f => f.IdTipoFuncionarioNavigation)
                .AsNoTracking()
                .FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Busca todos os funcionários e retorna uma lista de DTOs.
        /// </summary>
        /// <returns>Uma lista de DTOs de funcionários.</returns>
        public IEnumerable<FuncionarioDTO> GetAll()
        {
            var funcionarios = context.Funcionarios
                .Select(f => new FuncionarioDTO
                {
                    Id = f.Id,
                    Nome = f.Nome,
                    Telefone1 = f.Telefone1,
                    NomeRestaurante = f.IdRestauranteNavigation.Nome,
                    NomeTipoFuncionario = f.IdTipoFuncionarioNavigation.Nome
                })
                .ToList();
            return funcionarios;
        }
    }
}
