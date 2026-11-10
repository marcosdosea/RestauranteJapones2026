using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class FormaPagamentoService : IFormaPagamentoService
    {
        private readonly RestauranteContext context;

        public FormaPagamentoService(RestauranteContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Criar uma nova forma de pagamento
        /// </summary>
        /// <param name="formaPagamento">Dados da forma de pagamento</param>
        /// <returns>id da forma de pagamento criada</returns>
        public uint Create(Formapagamento formaPagamento)
        {
            context.Add(formaPagamento);
            context.SaveChanges();
            return formaPagamento.Id;
        }

        /// <summary>
        /// Remover uma forma de pagamento da base de dados
        /// </summary>
        /// <param name="id">Id da forma de pagamento</param>
        public void Delete(uint id)
        {
            var formaPagamento = context.Formapagamentos.Find(id);
            if (formaPagamento != null)
            {
                context.Remove(formaPagamento);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Editar uma forma de pagamento na base de dados
        /// </summary>
        /// <param name="formaPagamento">Dados da forma de pagamento</param>
        public void Edit(Formapagamento formaPagamento)
        {
            context.Update(formaPagamento);
            context.SaveChanges();
        }

        /// <summary>
        /// Buscar uma forma de pagamento na base de dados
        /// </summary>
        /// <param name="id">Id da forma de pagamento</param>
        /// <returns>Retorna a forma de pagamento encontrada ou null</returns>
        public Formapagamento Get(uint id)
        {
            return context.Formapagamentos.FirstOrDefault(f => f.Id == id)!;
        }

        /// <summary>
        /// Buscar todas as formas de pagamento na base de dados
        /// </summary>
        /// <returns>Retorna uma lista de todas as formas de pagamento</returns>
        public IEnumerable<Formapagamento> GetAll()
        {
            return context.Formapagamentos.AsNoTracking();
        }
    }
}
