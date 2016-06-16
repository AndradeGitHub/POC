using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DepencyInjectionWithUnity.domain;
using DepencyInjectionWithUnity.domain.model;
using DepencyInjectionWithUnity.infrastructure.Persistence;

namespace DepencyInjection.domain.Tests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void GetUser_SUCESS()
        {
            var unitOfWork = new UnitOfWork();

            var _domainFactory = DomainFactory.CreateDomain<UserDomainModel, User>(unitOfWork);

            var ret = _domainFactory.Get();
        }
    }
}
