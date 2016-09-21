using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DepencyInjectionWithUnity.domain.model;
using DepencyInjectionWithUnity.domain.repository;
using DepencyInjectionWithUnity.domain.repository.interfaces;
using DepencyInjectionWithUnity.infrastructure.Persistence;
using DepencyInjectionWithUnity.infrastructure.Persistence.interfaces;

namespace DepencyInjection.domain.Tests
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void GetUser_SUCESS()
        {
            var unitOfWork = new UnitOfWork();

            var _repositoryFactory = RepositoryFactory.CreateRepository<UserDomainModel, UserRepository>(unitOfWork);

            var userDomainModel = new UserDomainModel();

            var ret = _repositoryFactory.Get(userDomainModel);
        }
    }
}

