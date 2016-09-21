using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using DepencyInjectionWithUnity.domain.interfaces;
using DepencyInjectionWithUnity.infrastructure.Persistence;
using DepencyInjectionWithUnity.infrastructure.Persistence.interfaces;

namespace DepencyInjectionWithUnity.domain
{
    public static class DomainFactory
    {
        public static IOperation<T> CreateDomain<T, O>(IUnitOfWork unitOfWork)
        {
            IUnityContainer container = new UnityContainer();

            container.LoadConfiguration();
            
            container.RegisterType(typeof(IOperation<T>), typeof(O)).RegisterInstance(unitOfWork);

            return container.Resolve<IOperation<T>>();
        }
    }
}
