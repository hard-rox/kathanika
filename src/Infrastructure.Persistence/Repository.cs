using Kathanika.Domain.Repositories;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence;

internal class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        // public async Task<T> GetByIdAsync(Guid id)
        // {
        //     var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        //     return await _collection.Find(filter).SingleOrDefaultAsync();
        // }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        // public async Task UpdateAsync(T entity)
        // {
        //     var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        //     await _collection.ReplaceOneAsync(filter, entity);
        // }

        // public async Task DeleteAsync(T entity)
        // {
        //     var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        //     await _collection.DeleteOneAsync(filter);
        // }
    }