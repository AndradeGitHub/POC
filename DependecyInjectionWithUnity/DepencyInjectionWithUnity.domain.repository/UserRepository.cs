using System;
using System.Collections.Generic;
//using System.IdentityModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using DepencyInjectionWithUnity.infrastructure.Persistence;
using DepencyInjectionWithUnity.domain.model;

namespace DepencyInjectionWithUnity.domain.repository
{
    public class UserRepository : Repository<UserDomainModel>
    {
        private readonly UnitOfWork _db;

        public UserRepository(UnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public override List<UserDomainModel> Get(UserDomainModel user)
        {
            var ret = new List<UserDomainModel>();

            return ret;
        }
    }
}