using System.Linq.Expressions;
using ExchangeMongo.Documents;
using MongoDB.Driver.Linq;

namespace ExchangeMongo;

public interface IMongoRepository<TDocument> where TDocument : class, IDocument
{
    Task<bool> AnyAsync(Expression<Func<TDocument, bool>> filter = null);

    Task<IList<TDocument>> GetAsync(Expression<Func<TDocument, bool>> filter = null);

    Task<TDocument> GetFirstOrDefaultAsync(Expression<Func<TDocument, bool>> filter = null);

    Task AddAsync(TDocument document);

    Task DeleteAsync(Expression<Func<TDocument, bool>> filter);
    IMongoQueryable<TDocument> GetQuery();
}