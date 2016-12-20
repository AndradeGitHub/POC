using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;
using MongoDB.Bson;

using mongodb.domain.model;
using mongodb.infrastructure.persistence.interfaces;

namespace mongodb.domain.repository
{
    public class RepositoryColTeste<TEntity> : IRepositoryCustom<TEntity> where TEntity : EntityTeste
    {
        private IMongoCollection<TEntity> _collection;

        public RepositoryColTeste(IMongoDatabase repositoryBase, string collection)
        {
            _collection = repositoryBase.GetCollection<TEntity>(collection);
        }

        public IEnumerable<TEntity> GetWhere(TEntity entity)
        {            
            return _collection.Find(x => x.Log == entity.Log, null).ToListAsync<TEntity>().Result;
        }
    }
}