using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IServiceBase<TEntity>
    {
     
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
        Task<TEntity> GetById(int ind);
        IEnumerable<TEntity> GetAll();
    }
}
