using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework_Unity.comum.entidade;

namespace EntityFramework_Unity.dominio.repositorio
{
    public interface IRepositorio<T>
    {
        void Gravar(List<T> registro);
        List<T> RetornarTudo();

        //void Save();
    }
}
