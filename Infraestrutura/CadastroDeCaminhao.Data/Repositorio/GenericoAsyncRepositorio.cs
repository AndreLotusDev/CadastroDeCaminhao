using CadastroDeCaminhao.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Data.Repositorio
{
    public class GenericoAsyncRepositorio<T> : IGenericoAsyncRepositorio<T> where T : class
    {
        protected readonly Contexto.Banco _contexto;

        public GenericoAsyncRepositorio(Contexto.Banco _dbContext)
        {
            this._contexto = _dbContext;
        }

        public virtual IQueryable<T> Entidade => _contexto.Set<T>();
        public virtual async Task<T> PegaPorIdAsync(Guid id)
        {
            return await _contexto.Set<T>().FindAsync(id);
        }
        public virtual async Task<int> CountTotalAsync()
        {
            return await EntityFrameworkQueryableExtensions.CountAsync(_contexto.Set<T>());
        }
        public virtual async Task<IReadOnlyList<T>> PegaTodosAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_contexto.Set<T>());
        }
        public virtual async Task<T> AdicionaAsync(T entity)
        {
            await _contexto.Set<T>().AddAsync(entity);
            return entity;
        }
        public virtual async Task AtualizaAsync(T entity)
        {
            _contexto.Entry(entity).CurrentValues.SetValues(entity);
            await Task.CompletedTask;
        }
        public virtual async Task DeletaAsync(T entity)
        {
            _contexto.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

    }
}
