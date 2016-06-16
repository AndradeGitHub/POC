using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using DepencyInjectionWithUnity.domain.model;

namespace DepencyInjectionWithUnity.infrastructure.Persistence.interfaces
{
    public interface IUnitOfWork
    {        
        IDbSet<UserDomainModel> Users { get; set; }

        void Commit();
    }
}
