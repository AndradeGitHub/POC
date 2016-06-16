
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepencyInjectionWithUnity.domain.repository.interfaces
{
    public interface IRepository<T>
    {        
        List<T> Get(T entity);        
    }
}
