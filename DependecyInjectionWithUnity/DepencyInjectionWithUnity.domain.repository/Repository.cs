using System;
using System.Collections.Generic;
using System.Linq;

using DepencyInjectionWithUnity.domain.repository.interfaces;

namespace DepencyInjectionWithUnity.domain.repository
{
    public class Repository<T> : IRepository<T>
    {
        public virtual List<T> Get(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
