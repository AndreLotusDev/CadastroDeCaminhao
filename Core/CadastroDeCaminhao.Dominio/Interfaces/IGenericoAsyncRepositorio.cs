using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Dominio.Interfaces
{
    public interface IGenericoAsyncRepositorio<T> where T : class
    {
        /// <summary>
        ///     Iqueryable entity of Entity Framework. Use this to execute query in database level.
        /// </summary>
        IQueryable<T> Entidade { get; }
        Task<T> PegaPorIdAsync(Guid id);
        Task<int> CountTotalAsync();
        Task<IReadOnlyList<T>> PegaTodosAsync();
        Task<T> AdicionaAsync(T entity);
        Task AtualizaAsync(T entity);
        Task DeletaAsync(T entity);
    }
}
