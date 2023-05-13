using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Monolithic.Core.Infrastructure.Contracts;

namespace Monolithic.Core.InfraScture.Context;
public class MonogoDbContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    public MonogoDbContext(IOptions<MongoOptions> options)
    {
        var settings = options.Value;
        _client = new MongoClient(settings.ConnectionString);
        _database = _client.GetDatabase(settings.DatabaseName);
    }
    public IMongoClient Client => _client;
    public IMongoDatabase Database => _database;
}