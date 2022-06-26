using Application.Common.Interfaces;

using Domain.Common;

using MongoDB.Driver;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class MongoCollection<T> : IEntityCollection<T> where T : BaseEntity
    {
        public IMongoCollection<T> Collection { get; }

        public MongoCollection(IMongoCollection<T> collection)
        {
            Collection = collection;
        }
        public Task<Guid> Add(T entity)
        {
            Collection.InsertOne(entity);
            return Task.FromResult(entity.Id);
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
