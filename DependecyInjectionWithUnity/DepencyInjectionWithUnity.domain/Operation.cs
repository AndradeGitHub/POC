using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DepencyInjectionWithUnity.domain.interfaces;

namespace DepencyInjectionWithUnity.domain
{
    public class Operation<T> : IOperation<T>
    {
        public virtual List<T> Get()
        {
            throw new NotImplementedException();
        }
    }
}
