using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace EntityFramework_Unity.dominio.repositorio
{
    public static class FabricaDeRepositorioUnity
    {
        public static IRepositorio<T> Criar<T, R>(RepositorioEntityFramework repositorioModel)
        {
            IUnityContainer container = new UnityContainer();

            container.LoadConfiguration();
            container.RegisterType(typeof(IRepositorio<T>), typeof(R)).RegisterInstance(repositorioModel);                                                                                    
            
            return container.Resolve<IRepositorio<T>>();
        }        
    }
}
