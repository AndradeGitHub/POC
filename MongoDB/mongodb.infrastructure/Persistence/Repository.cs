using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;
using MongoDB.Bson;

using mongodb.domain.model;
using mongodb.infrastructure.persistence.interfaces;

namespace mongodb.infrastructure.persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {            
        private IMongoCollection<TEntity> _collection;

        public Repository(IMongoDatabase repositoryBase, string collection)
        {            
            _collection = repositoryBase.GetCollection<TEntity>(collection);
        }

        public async Task Insert(TEntity entity)
        {   
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(TEntity entity)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity, null);
        }

        public async Task Delete(TEntity entity)
        {
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _collection.Find(new BsonDocument(), null).ToListAsync<TEntity>().Result;
        }

        public TEntity GetById(int id)
        {
            return _collection.Find(x => x.Id == id, null).ToListAsync<TEntity>().Result.FirstOrDefault();
        }
    }
}