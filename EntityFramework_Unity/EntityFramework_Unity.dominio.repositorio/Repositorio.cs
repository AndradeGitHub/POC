using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework_Unity.dominio.repositorio
{
    public class Repositorio<T> : IRepositorio<T>
    {
        public virtual void Gravar(List<T> entidade)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> RetornarTudo()
        {
            throw new NotImplementedException();
        }                       
    }
}
