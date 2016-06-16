using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using DepencyInjectionWithUnity.domain.model;
using DepencyInjectionWithUnity.infrastructure.Persistence.Mapper;
using DepencyInjectionWithUnity.infrastructure.Persistence.interfaces;

namespace DepencyInjectionWithUnity.infrastructure.Persistence
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {        
        public virtual IDbSet<UserDomainModel> Users { get; set; }

        public UnitOfWork() : base("DiamondContext")
        {
        }

        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserDomainModelMap());            
        }
    }
}
