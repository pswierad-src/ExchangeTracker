using ExchangeApi.Models;
using ExchangeApi.Services.Interfaces;
using ExchangeMongo;
using ExchangeMongo.Documents;
using ExchangeMongo.Extensions;
using ExchangeRedis.Extensions;
using ExchangeShared;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ExchangeApi.Services;

public class TradeService : ITradeService
{
    private readonly IMongoRepository<TradeDocument> _tradeRepository;
    private readonly IDistributedCache _cache;

    public TradeService(IMongoRepository<TradeDocument> tradeRepository, IDistributedCache cache)
    {
        _tradeRepository = tradeRepository;
        _cache = cache;
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

    public async Task<IEnumerable<TradeLogResponse>> GetTradeLog(int amount)
    {
        var recordKey = "ExchangeLog_" + DateTime.Now.ToString("yyyyMMdd_hhmm");

        var logs = await _cache.GetRecordAsync<TradeLogResponse[]>(recordKey);

        if (logs is null)
        {
            logs = ( await _tradeRepository.GetQuery().Take(amount).ToListAsync())
                .Select(x => new TradeLogResponse
                {
                    ExchangeId = x.Id,
                    ExchangedProduct = x.ExchangedProduct,
                    Quantity = x.Quantity,
                    DateOfOperation = x.DateOfOperation
                })
                .ToArray();

            Console.WriteLine($"Loaded logs from database at {DateTime.Now}");

            await _cache.SetRecordAsync(recordKey, logs);
        }
        else
        {
            Console.WriteLine($"Loaded logs from cache at {DateTime.Now}");
        }

        return logs.AsEnumerable();
    }
}