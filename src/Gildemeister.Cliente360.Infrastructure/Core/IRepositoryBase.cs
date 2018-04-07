using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Infrastructure
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<bool> Insert(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Delete(int id);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        IQueryable<TEntity> ListAll();
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Dispose();
    }
}
