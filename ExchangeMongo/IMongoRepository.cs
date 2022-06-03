using System.Linq.Expressions;
using ExchangeMongo.Documents;

namespace ExchangeMongo;

public interface IMongoRepository<TDocument> where TDocument : class, IDocument
{
    Task<bool> AnyAsync(Expression<Func<TDocument, bool>> filter);

    Task<IList<TDocument>> GetAsync(Expression<Func<TDocument, bool>> filter);

    Task<TDocument> GetFirstOrDefaultAsync(Expression<Func<TDocument, bool>> filter);

    Task AddAsync(TDocument document);

    Task DeleteAsync(Expression<Func<TDocument, bool>> filter);
}