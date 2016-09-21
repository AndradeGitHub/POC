using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DepencyInjectionWithUnity.domain.model;
using DepencyInjectionWithUnity.domain.repository;
using DepencyInjectionWithUnity.infrastructure;
using DepencyInjectionWithUnity.infrastructure.Persistence;

namespace DepencyInjectionWithUnity.domain
{
    public class User : Operation<UserDomainModel>
    {
        private static dynamic _repositoryFactory;

        public User(UnitOfWork unitOfWork)
        {
            _repositoryFactory = RepositoryFactory.CreateRepository<UserDomainModel, UserRepository>(unitOfWork);
        }

        public override List<UserDomainModel> Get()
        {
            UserDomainModel userDomainModel = new UserDomainModel();

            userDomainModel.RequestDate = DateTime.Now;

            return _repositoryFactory.Get(userDomainModel);
        }
    }
}
