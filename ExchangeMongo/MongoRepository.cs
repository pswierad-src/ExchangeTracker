using System.Linq.Expressions;
using ExchangeMongo.Documents;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ExchangeMongo;

public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : class, IDocument 
{
    private const string ConnectionString = "mongodb://127.0.0.1:27017";
    private const string DatabaseName = "ExchangeTracker";

    private readonly IMongoCollection<TDocument> _collection;
    
    public MongoRepository()
    {
        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        _collection = db.GetCollection<TDocument>(typeof(TDocument).Name);
    }

    public async Task<bool> AnyAsync(Expression<Func<TDocument, bool>> filter = null)
    {
        var query = _collection.AsQueryable();
        
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return await query.AnyAsync();
    }

    public async Task<IList<TDocument>> GetAsync(Expression<Func<TDocument, bool>> filter = null)
    {
        var query = _collection.AsQueryable();
        
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<TDocument> GetFirstOrDefaultAsync(Expression<Func<TDocument, bool>> filter = null)
    {
        var query = _collection.AsQueryable();
        
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task AddAsync(TDocument document)
        => await _collection.InsertOneAsync(document);

    public async Task DeleteAsync(Expression<Func<TDocument, bool>> filter)
        => await _collection.DeleteManyAsync(filter);

    public IMongoQueryable<TDocument> GetQuery()
        => _collection.AsQueryable();
}