using MongoDB.Bson;
using MongoDB.Driver;
using Monolithic.Core.SharedKernel.Contract;
using Monolithic.Core.SharedKernel.DomainObjects;
using Monolithic.Core.SharedKernel.Enums;

namespace Monolithic.Core.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>  where TEntity : AbstractEntity
{
    private readonly IMongoCollection<TEntity> _collections;
    public FilterDefinition<TEntity> FilterDefinition { get; set; }
    public Repository(IMongoDatabase database)
    {
        FilterDefinition = Builders<TEntity>.Filter.Empty;
        _collections = database.GetCollection<TEntity>(typeof(TEntity).Name);
    }
    public async Task<TEntity> Find(string id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
        var entity = await _collections.FindAsync(filter);
        return await entity.FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<TEntity>> FindAll(string sortBy, ESortDirection sortDirection, int page, int offset)
        =>  await _collections
                    .Find(FilterDefinition)
                    .Sort(new BsonDocument(sortBy, (int)sortDirection))
                    .Skip(page * offset)
                    .Limit(offset)
                    .ToListAsync();
    public async Task<TEntity> Insert(TEntity entity)
    {
        await _collections.InsertOneAsync(entity);
        return entity;
    } 
    public async Task Update(TEntity entity, string id)
    {
        entity.UpdateAt = DateTime.UtcNow;
        var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
        await _collections.ReplaceOneAsync(filter, entity);
    }
    public async Task<long> CountDocuments()
        => await _collections.CountDocumentsAsync(FilterDefinition);
}