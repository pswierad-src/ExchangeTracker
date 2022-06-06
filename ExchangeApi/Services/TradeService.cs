using ExchangeApi.Models;
using ExchangeApi.Services.Interfaces;
using ExchangeMongo;
using ExchangeMongo.Documents;
using ExchangeMongo.Extensions;
using ExchangeShared;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ExchangeApi.Services;

public class TradeService : ITradeService
{
    private readonly IMongoRepository<TradeDocument> _tradeRepository;

    public TradeService(IMongoRepository<TradeDocument> tradeRepository)
    {
        _tradeRepository = tradeRepository;
    }

    public async Task<IEnumerable<Trade>> GetMostRecentTrades(TradeRequest request)
    {
        var trades = _tradeRepository.GetQuery();

        if (request.Type is not null)
        {
            trades = (IMongoQueryable<TradeDocument>)Queryable.Where(trades, x => x.Type == request.Type.GetValueOrDefault());
        }

        trades = trades.OrderByDescending(x => x.DateOfOperation).Take(request.Amount);

        return (await trades.ToListAsync()).Select(x => x.ToDomainObject());
    }
}