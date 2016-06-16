using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DepencyInjectionWithUnity.application;
using DepencyInjectionWithUnity.domain.model;

namespace DepencyInjection.facade.Tests
{
    [TestClass]
    public class UserFacadeTest
    {
        [TestCategory("DATABASE"), TestMethod]
        public void GetFacade_SUCESS()
        {
            var _userFacade = new UserFacade();

            var ret = _userFacade.Gel<UserDomainModel>();            
        }
    }
}
