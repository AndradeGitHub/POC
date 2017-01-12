using System.Collections.Generic;

namespace mongodb.infrastructure.persistence.interfaces
{
    public interface IRepositoryCustom<TEntity>
    {
        IEnumerable<TEntity> GetWhere(TEntity entity);

        int GetLastData();
    }
}