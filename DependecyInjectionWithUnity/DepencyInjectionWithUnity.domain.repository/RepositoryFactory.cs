using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using DepencyInjectionWithUnity.infrastructure.Persistence;
using DepencyInjectionWithUnity.infrastructure.Persistence.interfaces;
using DepencyInjectionWithUnity.domain.repository.interfaces;

namespace DepencyInjectionWithUnity.domain.repository
{
    public static class RepositoryFactory
    {
        public static IRepository<T> CreateRepository<T, R>(IUnitOfWork unitOfWork)
        {
            IUnityContainer container = new UnityContainer();

            container.LoadConfiguration();
            
            container.RegisterType(typeof(IRepository<T>), typeof(R)).RegisterInstance(unitOfWork);

            return container.Resolve<IRepository<T>>();
        }
    }
}