using System;
using System.Collections.Generic;

using DepencyInjectionWithUnity.domain;
using DepencyInjectionWithUnity.domain.model;
using DepencyInjectionWithUnity.domain.repository;
using DepencyInjectionWithUnity.infrastructure.Persistence;
using DepencyInjectionWithUnity.infrastructure.Persistence.interfaces;

namespace DepencyInjectionWithUnity.application
{
    public class UserFacade
    {
        private static UnitOfWork _unitOfWork;

        private static dynamic _domainFactory;
        private static dynamic _repositoryFactory;

        public UserFacade()
        {
            _unitOfWork = new UnitOfWork();

            _domainFactory = DomainFactory.CreateDomain<UserDomainModel, User>(_unitOfWork);
            _repositoryFactory = RepositoryFactory.CreateRepository<UserDomainModel, UserRepository>(_unitOfWork);
        }

        public List<T> Gel<T>()
        {
            try
            {
                var resultUserDomain = _domainFactory.Get();

                return resultUserDomain;
            }
            catch (Exception ex)
            {                
                throw (ex.InnerException);
            }
        }
    }
}
