using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepencyInjectionWithUnity.domain.interfaces
{
    public interface IOperation<T>
    {        
        List<T> Get();
    }
}