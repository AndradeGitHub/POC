using System.Threading.Tasks;
using System.Collections.Generic;

namespace mongodb.infrastructure.persistence.interfaces
{
    public interface IRepository<TEntity> 
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
    }
}