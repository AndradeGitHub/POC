using Microsoft.Practices.Unity;

using mongodb.infrastructure.persistence.interfaces;

namespace mongodb.infrastructure.persistence
{
    public static class RepositoryFactory
    {
        public static IRepository<T> CreateRepository<T, R>(dynamic repositoryBase, string collection)
        {            
            IUnityContainer container = new UnityContainer();
            
            container.RegisterType(typeof(IRepository<T>), typeof(R), new InjectionConstructor(repositoryBase, collection));

            return container.Resolve<IRepository<T>>();
        }

        public static IRepositoryCustom<T> CreateRepositoryCustom<T, R>(dynamic repositoryBase, string collection)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IRepositoryCustom<T>), typeof(R), new InjectionConstructor(repositoryBase, collection));

            return container.Resolve<IRepositoryCustom<T>>();
        }
    }
}