using Kathanika.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence;

internal abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;
    private readonly ILogger<Repository<T>> _logger;

    public Repository(IMongoDatabase database, string collectionName, ILogger<Repository<T>> logger)
    {
        _collection = database.GetCollection<T>(collectionName.ToLower());
        _logger = logger;
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(string id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        await _collection.DeleteOneAsync(filter);
    }
}